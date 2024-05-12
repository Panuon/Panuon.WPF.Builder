using Panuon.WPF.Builder.Internal.Utils;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;

namespace Panuon.WPF.Builder.Models
{
    internal abstract class DocumentTextElement
        : Element
    {
        protected DocumentTextElement(IAppBuilder appBuilder, IDictionary<string, object> config) 
            : base(appBuilder, config)
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

            var finalValue = SerializeUtil.SerializeValue(AppBuilder, property.PropertyType, value);

            if (finalValue is BindingBase binding)
            {
                element.SetBinding(property, binding);
            }
            else
            {
                element.SetValue(property, finalValue);
            }

        }

        protected void SetFactoryValue(FrameworkElementFactory factory,
            DependencyProperty property,
            object value)
        {
            if (value is Binding bindingValue)
            {
                factory.SetBinding(property, bindingValue);
                return;
            }

            var finalValue = SerializeUtil.SerializeValue(AppBuilder, property.PropertyType, value);

            if (finalValue is BindingBase binding)
            {
                factory.SetBinding(property, binding);
            }
            else
            {
                factory.SetValue(property, finalValue);
            }

        }
    }
}
