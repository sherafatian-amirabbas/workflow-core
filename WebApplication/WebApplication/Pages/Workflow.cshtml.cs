using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Dynamic;
using System.Security.Cryptography;
using System.Threading;
using WebApplication.DB;
using WebApplication.Pages.Models;
using WebApplication.Workflow;
using WorkflowCore.Interface;
using WorkflowCore.Services.DefinitionStorage;

namespace WebApplication.Pages
{
    public class WorkflowModel : PageModel
    {
        private readonly WorkflowHostProxy workflowHostProxy;
        private readonly IPersistenceProvider persistenceProvider;

        public WorkflowModel(WorkflowHostProxy workflowHostProxy, 
            IPersistenceProvider persistenceProvider)
        {
            this.workflowHostProxy = @workflowHostProxy;
            this.persistenceProvider = @persistenceProvider;
        }


        [BindProperty(Name = "wid", SupportsGet = true)]
        public string WorkflowId { get; set; }

        [BindProperty(Name = "wv", SupportsGet = true)]
        public int WorkflowVersion { get; set; }


        public void OnGet()
        {
        }

        public void OnPost()
        {
            var instanceId = this.workflowHostProxy.StartWorkflowAsync(WorkflowId, WorkflowVersion).Result;
            var instance = this.persistenceProvider.GetWorkflowInstance(instanceId).Result;
        }
    }
}
