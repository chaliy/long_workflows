using System;

namespace LongWorkflows
{
    public static class WorkflowServiceExtensions
    {
        public static void Run<T>(this IWorkflowService @this, string key, Action<T> exe) 
            where T : class
        {
            var instance = @this.Get<T>(key);
            exe(instance);
        }

        public static TRes Run<T, TRes>(this IWorkflowService @this, string key, Func<T, TRes> exe)
            where T : class
        {
            var instance = @this.Get<T>(key);
            return exe(instance);
        }
    }
}
