using Microsoft.Extensions.DependencyInjection;
using builder = Microsoft.AspNetCore.Builder;

using WebApplication.Workflow;
using WorkflowCore.Interface;
using System;

namespace WebApplication
{
    public static class ServiceConfiguration
    {
        public static void ConfigureService(this builder.WebApplication app)
        {
            // from WorkflowCore
            var hostProxy = app.Services.GetService<WorkflowHostProxy>();
            hostProxy.Host.Start();
        }
    }
}
