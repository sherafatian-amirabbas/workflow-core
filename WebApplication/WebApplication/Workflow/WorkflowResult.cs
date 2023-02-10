namespace WebApplication.Workflow
{
    public class WorkflowResult
    {
        public string WorkflowInstanceId { get; set; }
        public int StatusCode { get; set; }
        public string Content { get; set; }
        public string ContentType { get; set; }
    }
}
