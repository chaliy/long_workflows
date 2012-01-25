using System;
using LongWorkflows;
using NUnit.Framework;
using FluentAssertions;

namespace Tests
{
    public class When_run_simple_workflow_with_crash
    {
        private WorkflowService _service;        
        private Exception _exception;
        private const string SimpleWorkflowKey = "When_run_simple_workflow_with_crash";

        public class FooWorkflow
        {
            public virtual void Crash()
            {
                throw new Exception("With love from Workflow");
            }
        }

        [TestFixtureSetUp]
        public void Given_simple_workflow_runned_twice()
        {
            _service = new WorkflowService();
            var instance1 = _service.Get<FooWorkflow>(SimpleWorkflowKey);
            try
            {
                instance1.Crash();
            }
            catch (Exception ex)
            {
                _exception = ex;                
            }            
        }

        [Test]
        public void Should_raise_exception()
        {
            _exception.Should().NotBeNull();
        }

        [Test]
        public void Should_raise_exception_with_message()
        {
            _exception.Message.Should().Be("With love from Workflow");
        }
    }
}
