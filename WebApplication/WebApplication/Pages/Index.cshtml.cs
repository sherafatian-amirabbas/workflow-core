using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

using WorkflowCore.Interface;

using WebApplication.Pages.Models;
using WorkflowCore.Services.DefinitionStorage;
using WebApplication.DB;
using static System.Formats.Asn1.AsnWriter;
using WebApplication.Workflow;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Dynamic;
using System.Text.Json.Nodes;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;
using System;
using System.Data;
using System.Linq.Expressions;
using WorkflowCore.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebApplication.Pages
{
    public class IndexModel : PageModel
    {
        private readonly WorkflowHelper workflowHelper;
        private readonly WorkflowHostProxy workflowHostProxy;


        public IndexModel(WorkflowHelper workflowHelper,
            WorkflowHostProxy workflowHostProxy)
        {
            this.workflowHelper = @workflowHelper;
            this.workflowHostProxy = @workflowHostProxy;
        }


        [BindProperty]
        public DefinitionModel Input { get; set; }


        public IActionResult OnPost()
        {
            if (Request.Form["submit"] == "Register")
            {
                // async execution

                var def = this.workflowHelper.RegisterWorkflow(Input.Description);
                return new RedirectToPageResult($"/Workflow", new { wid = def.Id, wv = def.Version });
            }
            else
            {
                // sync execution

                var def = this.workflowHelper.RegisterWorkflow(Input.Description);
                var result = this.workflowHostProxy.StartWorkflow(def.Id, def.Version);
                return new ContentResult()
                {
                    StatusCode = result.StatusCode,
                    Content = result.Content,
                    ContentType = result.ContentType
                };
            }
        }
    }
}