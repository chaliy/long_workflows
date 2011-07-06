namespace LongWorkflows
{
    public interface IWorkflowService
    {
        T Get<T>(string key) where T : class;
    }
}
