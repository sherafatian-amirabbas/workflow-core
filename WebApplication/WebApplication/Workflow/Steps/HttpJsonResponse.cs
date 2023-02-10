using System;

using WorkflowCore.Interface;
using WorkflowCore.Models;

using WebApplication.Caller.Http.Models;
using WebApplication.Caller.Http.Service.Interface;
using WebApplication.Caller.Http.Helper.Interface;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace WebApplication.Workflow.Steps
{
    public class HttpJsonResponse : StepBody
    {
        private readonly IHttpContextAccessor contextAccessor;

        public HttpJsonResponse(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = @contextAccessor;
        }

        
        public int ResponseCode { get; set; }
        public dynamic ResponseContent { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            var result = (WorkflowResult)((WorkFlowBag)context.Workflow.Data)["Result"];
            result.StatusCode = this.ResponseCode;
            result.Content = JsonConvert.SerializeObject(this.ResponseContent);
            result.ContentType = "application/json";

            return ExecutionResult.Next();
        }

    }
}