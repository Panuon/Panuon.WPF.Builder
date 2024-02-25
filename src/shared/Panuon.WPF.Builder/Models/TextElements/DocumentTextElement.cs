using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;

namespace Panuon.WPF.Builder.Models
{
    internal abstract class DocumentTextElement
        : Element
    {
        protected DocumentTextElement(IDictionary<string, object> config) : base(config)
        {
        }

        protected void SetValue(TextElement element,
            DependencyProperty property,
            object value)
        {
            
            if (value is Binding bindingValue)
            {
                element.SetBinding(property, bindingValue);
                return;
            }

            var finalValue = SerializeValue(property.PropertyType, value);

            if (finalValue is BindingBase binding)
            {
                element.SetBinding(property, binding);
            }
            else
            {
                element.SetValue(property, finalValue);
            }

        }
    }
}
