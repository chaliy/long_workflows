using System;
using System.Reactive;

namespace Lrw
{
    public static class WorkflowServiceExtensions
    {
        public static void Run<T>(this IWorkflowService @this, string key, Action<T> exe, Action<T> init = null)
        {
            @this.Run(key, x => { exe(x); return Unit.Default; }, init);
        }
    }
}
