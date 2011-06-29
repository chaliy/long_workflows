using System.Net;
using Lrw;

namespace SampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var reg = new WorkflowRegistry(new Conventions
                                               {
                                                   
                                               });

            var workflow = reg.GetWorkflow<PingGoogleForewer>("PingGoogleForewer");

            workflow.Next();
        }
    }

    public class PingGoogleForewer : AbstractWorkflow
    {
        protected override void ProcessNext()
        {
            new WebClient().DownloadString("http://www.google.com.ua");
        }
    }
}
