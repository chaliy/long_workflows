using System;

namespace Lrw
{
    public class Conventions
    {
        public Func<Type, object> CreateInstance { get; set; }        
        public IWorkflowStateStore StateStore { get; set; }        

        public Conventions()
        {
            CreateInstance = Activator.CreateInstance;            
            StateStore = new MemoryWrorkflowStateStore();
        }
    }
}
