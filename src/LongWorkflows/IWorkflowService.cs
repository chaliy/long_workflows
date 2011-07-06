using System;

namespace LongWorkflows
{
    public interface IWorkflowService
    {
        TRes Run<T, TRes>(string key, Func<T, TRes> exe, Action<T> init = null);
    }
}
