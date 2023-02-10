using System;

using WorkflowCore.Interface;
using WorkflowCore.Models;

using WebApplication.Caller.Http.Models;
using WebApplication.Caller.Http.Service.Interface;
using WebApplication.Caller.Http.Helper.Interface;
using System.Collections.Generic;
using System.Net.Http;
using WebApplication.Workflow.Steps.BaseSteps;
using System.Web;
using WebApplication.Caller.Http.Authentication;

namespace WebApplication.Workflow.Steps
{
    public class HttpGetRequest : HttpRequestStepBody
    {
        public HttpGetRequest(IHttpService httpService,
            IHttpServiceHelper httpServiceHelper)
            : base(httpService, httpServiceHelper)
        { }


        public string UrlParams { get; set; }


        public override ExecutionResult Run(IStepExecutionContext context)
        {
            BuildUrl();

            AuthenticationScheme scheme = null;
            if (!string.IsNullOrEmpty(this.Bearer))
                scheme = new BearerAuthenticationScheme(this.Bearer);

            var responseMessage = this.httpService.Get(new HttpClientRequest(
                this.Url,
                scheme,
                null,
                null));

            this.ResponseCode = (int)responseMessage.StatusCode;

            var response = this.httpServiceHelper.GetMessageResponseAsString(responseMessage);

            if (this.ResponseType == "JSON")
                this.ResponseContent = response.JsonToExpando();

            return ExecutionResult.Next();
        }


        private void BuildUrl()
        {
            var items = this.UrlParams.JsonToExpando();
            foreach (var item in items)
                this.Url = this.Url.Replace("{{" + item.Key + "}}", item.Value.ToString());
        }
    }
}