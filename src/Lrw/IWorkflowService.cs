namespace Lrw
{
    public interface IWorkflowService
    {
        void Run<T>(string key)
            where T : IWorkflow;
    }
}
