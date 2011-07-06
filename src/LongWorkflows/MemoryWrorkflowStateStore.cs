using System.Collections.Generic;

namespace LongWorkflows
{
    public class MemoryWrorkflowStateStore : IWorkflowStateStore
    {
        private readonly Dictionary<string, WorkflowState> _store = new Dictionary<string, WorkflowState>();

        public WorkflowState Get(string key)
        {
            if (_store.ContainsKey(key))
            {
                return _store[key];    
            }
            return null;
        }

        public void Store(string key, WorkflowState state)
        {
            _store[key] = state;
        }
    }
}
