using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;

namespace LongWorkflows
{
    public class WorkflowService : IWorkflowService
    {
        private readonly Conventions _conventions;
        private readonly ProxyGenerator _generator = new ProxyGenerator();
        
        public WorkflowService(Conventions conventions = null)
        {
            _conventions = conventions ?? new Conventions();
        }

        public Conventions Conventions
        {
            get { return _conventions; }
        }
        
        public T Get<T>(string key) where T : class
        {
            var instance = (T)_conventions.CreateInstance(typeof(T));

            RetrieveWorkflow(key, instance, null);

            var interceptor = new NotifyChangesInterceptor(() => StoreWorkflow(key, instance)); 
            
            return _generator.CreateClassProxyWithTarget(instance, interceptor);
        }
        
        private void RetrieveWorkflow<T>(string key, T instance, Action<T> init)
        {
            var state = _conventions.StateStore.Get(key);

            if (state == null)
            {
                if (init != null)
                {
                    init(instance);
                }
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

        private class NotifyChangesInterceptor : IInterceptor
        {
            private readonly Action _changed;

            public NotifyChangesInterceptor(Action changed)
            {
                _changed = changed;
            }

            public void Intercept(IInvocation invocation)
            {                
                invocation.Proceed();
                _changed();
            }
        }
    }
}
