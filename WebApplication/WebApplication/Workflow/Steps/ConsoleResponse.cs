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
    public class ConsoleResponse : StepBody
    {
        private readonly IHttpContextAccessor contextAccessor;

        public ConsoleResponse(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = @contextAccessor;
        }

        public string Message { get; set; }

        public override ExecutionResult Run(IStepExecutionContext context)
        {
            Console.Write(Message);

            return ExecutionResult.Next();
        }

    }
}