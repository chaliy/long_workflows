using System;

namespace Lrw
{
    public interface IWorkflowService
    {
        TRes Run<T, TRes>(string key, Func<T, TRes> exe, Action<T> init = null);
    }
}
