using LongWorkflows;
using NUnit.Framework;
using FluentAssertions;

namespace Tests
{
    public class When_run_simple_workflow
    {
        private WorkflowService _service;
        private const string SimpleWorkflowKey = "When_run_simple_workflow";

        public class FooWorkflow
        {
            public int Executed { get; set; }

            public virtual void Ping()
            {
                Executed++;
            }
        }

        [TestFixtureSetUp]
        public void Given_simple_workflow_runned_twice()
        {
            _service = new WorkflowService();
            var instance1 = _service.Get<FooWorkflow>(SimpleWorkflowKey);
            instance1.Ping();

            var instance2 = _service.Get<FooWorkflow>(SimpleWorkflowKey);
            instance2.Ping();
        }

        [Test]
        public void Should_save_state_after_execution()
        {
            var state = _service.Conventions.StateStore.Get(SimpleWorkflowKey);
            ((int)state["Executed"]).Should().Be(2);
        }
    }
}
