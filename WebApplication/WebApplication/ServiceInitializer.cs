using Microsoft.Extensions.DependencyInjection;
using System;
using System.Runtime.CompilerServices;

using WebApplication.Caller.Http.Service;
using WebApplication.Caller.Http.Helper.Interface;
using WebApplication.Caller.Http.Helper;
using WebApplication.Caller.Http.Service.Interface;
using WebApplication.DB;
using WebApplication.Workflow;
using WebApplication.Workflow.Steps;
using WebApplication.Workflow.Steps.BaseSteps;
using Microsoft.AspNetCore.Http;

namespace WebApplication
{
    public static class ServiceInitializer
    {
        public static void InitializeServices(this IServiceCollection services)
        {
            // from WorkflowCore
            services.AddWorkflow();
            services.AddWorkflowDSL();
            services.AddSingleton<WorkflowHostProxy>();

            services.AddHttpClient<CallerHttpClient>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<IHttpServiceHelper, HttpServiceHelper>();
            services.AddSingleton<IHttpService, HttpService>();

            services.AddSingleton<IStore, Store>();


            services.AddSingleton<WorkflowHelper>();
            services.AddSingleton<DefinitionFactory>();
            

            services.AddTransient<HttpPostRequest>();
            services.AddTransient<HttpGetRequest>();
            services.AddTransient<HttpJsonResponse>();
            services.AddTransient<ConsoleResponse>();
        }
    }
}
