using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace WebApplication.Workflow
{
    public class WorkFlowBag
    {
        private readonly Dictionary<string, dynamic> dic;

        public WorkFlowBag()
        {
            dic = new Dictionary<string, dynamic>();

            dic.Add("Result", new WorkflowResult());
        }

        public dynamic this[string key]
        {
            get
            {
                return dic[key];
            }
            set
            {
                dic[key] = value;
            }
        }
    }
}
