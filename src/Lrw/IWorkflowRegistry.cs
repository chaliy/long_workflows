namespace Lrw
{
    public interface IWorkflowRegistry
    {        
        T GetWorkflow<T>(string key);
    }
}
