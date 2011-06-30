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
            reg.Run<PingForewer>("PingForewer");
            reg.Run<PingForewer>("PingForewer");
        }

        private static void Session2(WorkflowService reg)
        {
            reg.Run<PingForewer>("PingForewer");
        }
    }

    public class PingForewer : IWorkflow
    {
        public int Counter { get; set; }
        
        public void Next()
        {
            Counter++;
            Console.WriteLine("Ping #{0}", Counter);
        }
    }
}
