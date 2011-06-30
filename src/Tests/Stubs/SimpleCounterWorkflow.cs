using Lrw;


namespace Tests.Stubs
{
    public class SimpleCounterWorkflow : IWorkflow
    {
        public int Counter { get; set; }

        public void Next()
        {
            Counter++;
        }
    }
}
