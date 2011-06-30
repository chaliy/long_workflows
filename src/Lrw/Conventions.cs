using System;

namespace Lrw
{
    public class Conventions
    {
        public Func<Type, object> CreateInstance { get; set; }        
        public IWorkflowStateStore StateStore { get; set; }
        public Action<dynamic> InvokeNext { get; set; }

        public Conventions()
        {
            CreateInstance = Activator.CreateInstance;            
            StateStore = new MemoryWrorkflowStateStore();
            InvokeNext = x => x.Next();
        }
    }
}
