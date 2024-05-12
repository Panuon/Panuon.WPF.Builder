using System;
using System.Collections.Generic;
using System.Windows;

namespace Panuon.WPF.Builder.Elements
{
    internal class ControlElement<TElement>
        : Element, IControlElement
        where TElement : FrameworkElement
    {
        internal ControlElement(IAppBuilder appBuilder, IDictionary<string, object> config)
            : base(appBuilder, config)
        {

        }

        public override Type VisualType => typeof(TElement);
    }
}
