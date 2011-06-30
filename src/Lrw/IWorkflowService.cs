using System;

namespace Lrw
{
    public interface IWorkflowService
    {
        void Run<T>(string key, Action<T> exe);
    }
}
