using System;
using System.Reactive;
using System.Reactive.Subjects;

namespace Lrw
{
    public abstract class AbstractWorkflow
    {
        private readonly Subject<Unit> _flushedSubject = new Subject<Unit>();

        public IObservable<Unit> Flushed { get { return _flushedSubject; } }

        public void Next()
        {

            Complete();
        }

        protected abstract void ProcessNext();

        protected void Flush()
        {
            _flushedSubject.OnNext(Unit.Default);
        }

        protected void Complete()
        {
            _flushedSubject.OnCompleted();
        }
    }
}
