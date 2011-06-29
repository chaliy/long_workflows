using System;

namespace Lrw
{
    public class Conventions
    {
        public Func<Type, object> CreateInstance { get; set; }
        public Func<string, WorkflowState> GetState { get; set; }
    }
}
