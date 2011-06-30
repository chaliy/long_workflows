using Lrw;
using NUnit.Framework;
using FluentAssertions;

namespace Tests
{
    public class When_run_simple_workflow
    {
        private WorkflowService _service;
        private const string SimpleWorkflowKey = "When_run_simple_workflow";

        public class FooWorkflow : IWorkflow
        {
            public int Executed { get; set; }

            public void Next()
            {
                Executed++;
            }
        }

        [TestFixtureSetUp]
        public void Given_simple_workflow_runned_twice()
        {
            _service = new WorkflowService();
            _service.Run<FooWorkflow>(SimpleWorkflowKey);
            _service.Run<FooWorkflow>(SimpleWorkflowKey);
        }

        [Test]
        public void Should_save_state_after_execution()
        {
            var state = _service.Conventions.StateStore.Get(SimpleWorkflowKey);
            ((int)state["Executed"]).Should().Be(2);
        }
    }
}
