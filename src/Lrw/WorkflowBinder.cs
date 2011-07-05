using System;
using System.ComponentModel;
using System.Reflection;

namespace Lrw
{
    public class WorkflowBinder
    {
        private readonly object _instance;

        public WorkflowBinder(object instance)
        {
            _instance = instance;
        }

        public void Bind(WorkflowState state)
        {

            var worflowType = _instance.GetType();

            foreach (var name in state.Keys)
            {

                var val = state[name];

                if (val != null)
                {
                    var prop = worflowType.GetProperty(name, BindingFlags.Public
                                                         | BindingFlags.Instance);

                    if (prop != null && prop.CanRead && prop.CanWrite)
                    {
                        if (prop.PropertyType.IsAssignableFrom(val.GetType()))
                        {
                            prop.SetValue(_instance, val, null);
                        }
                        else
                        {
                            var converter = TypeDescriptor.GetConverter(prop.PropertyType);
                            if (converter != null && converter.CanConvertFrom(val.GetType()))
                            {
                                val = converter.ConvertFrom(val);
                            }

                            prop.SetValue(_instance, val, null);
                        }
                    }
                }
            }
        }
    }
}
