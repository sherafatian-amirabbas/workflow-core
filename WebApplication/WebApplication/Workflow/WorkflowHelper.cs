using WorkflowCore.Interface;
using WorkflowCore.Services.DefinitionStorage;

using WebApplication.DB;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Dynamic;
using WebApplication.Workflow.Steps;
using WorkflowCore.Models;

namespace WebApplication.Workflow
{
    public class WorkflowHelper
    {
        private readonly IStore store;
        private readonly DefinitionFactory definitionLoader;


        public WorkflowHelper(IStore store,
            DefinitionFactory definitionFactory)
        {
            this.store = @store;
            this.definitionLoader = @definitionFactory;
        }


        public WorkflowDefinition RegisterWorkflow(string description)
        {
            var def = this.definitionLoader.Create(description)
                .Initialize()
                .Parse()
                .Load();    

            _ = this.store.Add(description);
            
            return def;
        }
    }
}
