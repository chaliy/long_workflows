using System;
using LongWorkflows;

namespace SampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var reg = new WorkflowService();

            Session1(reg);
            Session2(reg);            
        }        

        private static void Session1(WorkflowService reg)
        {
            var instance = reg.Get<PingForewerWorkflow>("PingForewer");
            instance.Ping();
            instance.Ping();            
        }

        private static void Session2(WorkflowService reg)
        {
            var instance = reg.Get<PingForewerWorkflow>("PingForewer");
            instance.Ping();
        }
    }

    public class PingForewerWorkflow
    {
        public int Counter { get; set; }
        
        public virtual void Ping()
        {
            Counter++;
            Console.WriteLine("Ping #{0}", Counter);
        }
    }
}
