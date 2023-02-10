using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Dynamic;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Services.DefinitionStorage;

namespace WebApplication.Workflow.Steps
{
    public class DefinitionFactory
    {
        readonly IDefinitionLoader definitionLoader;
        readonly WorkflowHostProxy hostProxy;

        public DefinitionFactory(IDefinitionLoader definitionLoader,
            WorkflowHostProxy hostProxy)
        {
            this.definitionLoader = @definitionLoader;
            this.hostProxy = @hostProxy;
        }

        public Definition Create(string description)
        {
            return new Definition(this.definitionLoader,
                this.hostProxy,
                description);
        }
    }

    public class Definition
    {
        const string PROP_DATATYPE = "DataType";
        const string PROP_STEPS = "Steps";
        const string PROP_STEPTYPE = "StepType";
        const string STEP_HTTPPOST = nameof(HttpPostRequest);
        const string STEP_HTTPGET = nameof(HttpGetRequest);
        const string STEP_HTTPJSONRESPONSE = nameof(HttpJsonResponse);
        const string STEP_CONSOLERESPONSE = nameof(ConsoleResponse);


        readonly IDefinitionLoader definitionLoader;
        readonly WorkflowHostProxy hostProxy;
        dynamic expandoRepresentation;


        public Definition(IDefinitionLoader definitionLoader,
            WorkflowHostProxy hostProxy,
            string description)
        {
            this.definitionLoader = definitionLoader;
            this.hostProxy = @hostProxy;
            this.Description = description;
        }


        public string Description { get; }


        public Definition Initialize()
        {
            expandoRepresentation = this.Description.JsonToExpando();
            return this;
        }

        public Definition Parse()
        {
            SetDataType();
            SetStepTypes();
            return this;
        }

        public WorkflowDefinition Load()
        {
            string json = JsonConvert.SerializeObject(expandoRepresentation);

            if (this.hostProxy.Host.Registry.IsRegistered(expandoRepresentation.Id, (int)expandoRepresentation.Version))
                this.hostProxy.Host.Registry.DeregisterWorkflow(expandoRepresentation.Id, (int)expandoRepresentation.Version);

            return this.definitionLoader.LoadDefinition(json, Deserializers.Json);
        }


        #region Private Methods

        private void SetDataType()
        {
            expandoRepresentation.DataType = typeof(WorkFlowBag);
        }

        private bool HasProperty(ExpandoObject obj, string property)
        {
            return ((IDictionary<string, object>)obj).ContainsKey(property);
        }

        public void SetStepTypes()
        {
            if (HasProperty(expandoRepresentation, PROP_STEPS))
                foreach (var item in expandoRepresentation.Steps)
                {
                    if (HasProperty(item, PROP_STEPTYPE) &&
                        item.StepType.GetType() == typeof(string))
                    {
                        switch (item.StepType)
                        {
                            case STEP_HTTPPOST:
                                item.StepType = typeof(HttpPostRequest);
                                break;
                            case STEP_HTTPGET:
                                item.StepType = typeof(HttpGetRequest);
                                break;
                            case STEP_HTTPJSONRESPONSE:
                                item.StepType = typeof(HttpJsonResponse);
                                break;
                            case STEP_CONSOLERESPONSE:
                                item.StepType = typeof(ConsoleResponse);
                                break;
                            default:
                                break;
                        }
                    }
                }
        }

        #endregion
    }
}
