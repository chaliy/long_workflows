namespace Tests.Stubs
{
    public class SimpleCounterWorkflow
    {
        public int Counter { get; set; }

        public virtual void Ping()
        {
            Counter++;
        }
    }
}
