using System.Linq;
using System.Reflection;

namespace Lrw
{
    public class WorkflowService : IWorkflowService
    {
        private readonly Conventions _conventions;
        
        public WorkflowService(Conventions conventions = null)
        {
            _conventions = conventions ?? new Conventions();
        }

        public Conventions Conventions
        {
            get { return _conventions; }
        }

        public void Run<T>(string key)
        {
            var instance = (T)_conventions.CreateInstance(typeof(T));

            RetrieveWorkflow(key, instance);

            _conventions.InvokeNext(instance);

            StoreWorkflow(key, instance);
        }

        private void RetrieveWorkflow<T>(string key, T instance)
        {
            var state = _conventions.StateStore.Get(key) ?? new WorkflowState();
            var worflowType = typeof (T);
            foreach (var name in state.Keys)
            {
                var prop = worflowType.GetProperty(name, BindingFlags.Public
                                                         | BindingFlags.Instance);
                if (prop != null && prop.CanRead && prop.CanWrite)
                {
                    prop.SetValue(instance, state[name], null);
                }
            }
        }

        private void StoreWorkflow<T>(string key, T instance)
        {
            var worflowType = typeof(T);
            var state = new WorkflowState();
            foreach (var prop in worflowType.GetProperties(BindingFlags.Public 
                                                        | BindingFlags.Instance)
                                            .Where(x => x.CanWrite && x.CanRead))
            {
                state[prop.Name] = prop.GetValue(instance, null);
            }
            _conventions.StateStore.Store(key, state);
        }
    }
}
