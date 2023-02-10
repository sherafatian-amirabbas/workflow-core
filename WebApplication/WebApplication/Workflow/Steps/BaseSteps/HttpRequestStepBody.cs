using System;

using WorkflowCore.Interface;
using WorkflowCore.Models;

using WebApplication.Caller.Http.Models;
using WebApplication.Caller.Http.Service.Interface;
using WebApplication.Caller.Http.Helper.Interface;
using System.Collections.Generic;
using System.Net.Http;
using WebApplication.Caller.Http.Authentication;
using Newtonsoft.Json;
using System.Dynamic;

namespace WebApplication.Workflow.Steps.BaseSteps
{
    public abstract class HttpRequestStepBody : StepBody
    {
        protected readonly IHttpService httpService;
        protected readonly IHttpServiceHelper httpServiceHelper;

        protected HttpRequestStepBody(IHttpService httpService,
            IHttpServiceHelper httpServiceHelper)
        {
            this.httpService = @httpService;
            this.httpServiceHelper = @httpServiceHelper;
        }


        public string Url { get; set; }
        public string Bearer { get; set; }
        public int ResponseCode { get; set; }
        public dynamic ResponseContent { get; set; }
        public string ResponseType { get; set; }
    }
}