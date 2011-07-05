using System;
using System.ComponentModel;
using System.Globalization;
using Lrw;
using NUnit.Framework;
using FluentAssertions;

namespace Tests
{
    public class When_bind_workflow_to_state
    {        

        public class FooValueConverter : TypeConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string))
                {
                    return true;
                }
                return base.CanConvertFrom(context, sourceType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {                
                if (value is string)
                {
                    return new FooValue((string) value);
                }

                return base.ConvertFrom(context, culture, value);
            }
        }

        [TypeConverter(typeof(FooValueConverter))]
        public struct FooValue
        {
            private readonly string _value;

            public FooValue(string value)
            {
                _value = value;
            }

            public override string ToString()
            {
                return _value;
            }
        }

        public class FooWorkflow
        {
            public string Test1 { get; set; }
            public FooValue Test2 { get; set; }
        }
        
        private FooWorkflow _instance;

        [TestFixtureSetUp]
        public void Given_simple_workflow_instance()
        {
            _instance = new FooWorkflow();
            var state = new WorkflowState {{"Test1", "Value #1"}, {"Test2", "Value #2"}};

            new WorkflowBinder(_instance).Bind(state);
        }

        [Test]
        public void Should_read_simple_value()
        {
            _instance.Test1.Should().Be("Value #1");
        }


        [Test]
        public void Should_read_value_with_type_converter()
        {
            _instance.Test2.Should().Be(new FooValue("Value #2"));
        }
    }
}
