namespace Lrw
{
    public class WorkflowRegistry : IWorkflowRegistry
    {
        private readonly Conventions _conventions;

        public WorkflowRegistry(Conventions conventions)
        {
            _conventions = conventions;
        }

        public T GetWorkflow<T>(string key)
        {
            var instance = (T)_conventions.CreateInstance(typeof(T));

            var state = _conventions.GetState(key);            
            var worflowType = typeof(T);

            foreach (var name in state.Keys)
            {
                worflowType.GetProperty(name).SetValue(instance, state[name], null);
            }

            return instance;
        }
    }
}
