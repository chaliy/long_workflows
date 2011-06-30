using System;
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

        public void Run<T>(string key)
            where T : IWorkflow
        {
            var instance = (T)_conventions.CreateInstance(typeof(T));

            RetrieveWorkflow(key, instance);

            instance.Next();            

            StoreWorkflow(key, instance);
        }

        private void RetrieveWorkflow<T>(string key, T instance) where T : IWorkflow
        {
            var state = _conventions.StateStore.Get(key) ?? new WorkflowState();
            var worflowType = typeof (T);
            foreach (var name in state.Keys)
            {
                var prop = worflowType.GetProperty(name, BindingFlags.Public
                                                         | BindingFlags.Instance | BindingFlags.SetProperty);
                if (prop != null && prop.GetSetMethod() != null)
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
                                                        | BindingFlags.Instance
                                                        | BindingFlags.GetProperty
                                                        | BindingFlags.SetProperty))
            {
                state[prop.Name] = prop.GetValue(instance, null);
            }
            _conventions.StateStore.Store(key, state);
        }
    }
}
