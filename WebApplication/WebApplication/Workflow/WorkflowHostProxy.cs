using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models.LifeCycleEvents;
using WorkflowCore.Models;
using Microsoft.Extensions.Logging;

namespace WebApplication.Workflow
{
    public class WorkflowHostProxy
    {
        private readonly ILifeCycleEventHub eventHub;
        private readonly IPersistenceProvider persistenceProvider;
        private readonly Dictionary<string, TaskCompletionSource> completionSourceList;

        public WorkflowHostProxy(IWorkflowHost host,
            ILifeCycleEventHub eventHub,
            IPersistenceProvider persistenceProvider)
        {
            this.Host = host;
            this.eventHub = @eventHub;
            this.persistenceProvider = @persistenceProvider;

            this.completionSourceList = new();

            SubscribeToHub();
        }

        public IWorkflowHost Host { get; }


        public Task<string> StartWorkflowAsync(string workflowId, int? version)
        {
            return this.Host.StartWorkflow(workflowId, version, data: null);
        }

        public WorkflowResult StartWorkflow(string workflowId, int? version)
        {
            TaskCompletionSource tsc = new();

            var instanceId = string.Empty;

            try
            {
                instanceId = this.Host.StartWorkflow(workflowId, version, data: null).Result;
                this.completionSourceList.Add(instanceId, tsc);
            }
            catch (Exception ex)
            {
                tsc.TrySetException(ex);
            }

            tsc.Task.Wait();


            var instance = this.persistenceProvider.GetWorkflowInstance(instanceId).Result;
            var result = (WorkflowResult)((WorkFlowBag)instance.Data)["Result"];
            result.WorkflowInstanceId = instanceId;

            return result;
        }


        #region Private Methods

        private void SubscribeToHub()
        {
            this.eventHub.Subscribe(e =>
            {
                if (e is WorkflowCompleted or
                    WorkflowTerminated or
                    WorkflowError)
                {
                    if (!completionSourceList.TryGetValue(e.WorkflowInstanceId, out TaskCompletionSource tsc))
                        return;

                    completionSourceList.Remove(e.WorkflowInstanceId);
                    tsc.SetResult();
                }
            });
        }

        #endregion
    }
}
