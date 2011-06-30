namespace Tests.Stubs
{
    public class SimpleCounterWorkflow
    {
        public int Counter { get; set; }

        public void Ping()
        {
            Counter++;
        }
    }
}
