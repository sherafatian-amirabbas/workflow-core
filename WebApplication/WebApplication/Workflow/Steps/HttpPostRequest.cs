using System;

using WorkflowCore.Interface;
using WorkflowCore.Models;

using WebApplication.Caller.Http.Models;
using WebApplication.Caller.Http.Service.Interface;
using WebApplication.Caller.Http.Helper.Interface;
using System.Collections.Generic;
using System.Net.Http;
using WebApplication.Workflow.Steps.BaseSteps;

namespace WebApplication.Workflow.Steps
{
    public class HttpPostRequest : HttpRequestStepBody
    {
        public HttpPostRequest(IHttpService httpService,
            IHttpServiceHelper httpServiceHelper)
            : base(httpService, httpServiceHelper)
        { }


        public string Content { get; set; }
        public string RequestContentType { get; set; }


        public override ExecutionResult Run(IStepExecutionContext context)
        {
            var requestContent = GetContent();
            var responseMessage = this.httpService.Post(new HttpClientRequest(
                this.Url,
                null,
                null,
                requestContent));

            this.ResponseCode = (int)responseMessage.StatusCode;

            var response = this.httpServiceHelper.GetMessageResponseAsString(responseMessage);

            if (this.ResponseType == "JSON")
                this.ResponseContent = response.JsonToExpando();

            return ExecutionResult.Next();
        }


        private HttpContent GetContent()
        {
            if (this.RequestContentType == "FormUrlEncoded")
                return this.httpServiceHelper.GetHttpContent(this.Content);
            else if (this.RequestContentType == "JSON")
                return this.httpServiceHelper.GetJsonHttpContentFromJsonString(this.Content);
            else
                throw new NotImplementedException();
        }
    }
}