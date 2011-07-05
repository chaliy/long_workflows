using System;
using System.ComponentModel;
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

        public TRes Run<T, TRes>(string key, Func<T, TRes> exe, Action<T> init = null)
        {
            var instance = (T)_conventions.CreateInstance(typeof(T));

            RetrieveWorkflow(key, instance, init);

            var res = exe(instance);

            StoreWorkflow(key, instance);

            return res;
        }

        private void RetrieveWorkflow<T>(string key, T instance, Action<T> init)
        {
            var state = _conventions.StateStore.Get(key);

            if (state == null)
            {
                init(instance);
            }
            else
            {
                new WorkflowBinder(instance).Bind(state);
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
