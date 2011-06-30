using System;
using Lrw;

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
            reg.Run<PingForewerWorkflow>("PingForewer", x => x.Ping());
            reg.Run<PingForewerWorkflow>("PingForewer", x => x.Ping());
        }

        private static void Session2(WorkflowService reg)
        {
            reg.Run<PingForewerWorkflow>("PingForewer", x => x.Ping());
        }
    }

    public class PingForewerWorkflow
    {
        public int Counter { get; set; }
        
        public void Ping()
        {
            Counter++;
            Console.WriteLine("Ping #{0}", Counter);
        }
    }
}
