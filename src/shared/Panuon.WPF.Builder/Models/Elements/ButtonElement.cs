using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Panuon.WPF.Builder.Elements
{
    internal class ButtonElement
        : ContentControlElement, IButtonElement
    {
        #region Ctor
        internal ButtonElement(IDictionary<string, object> config)
            : base(config)
        {
        }
        #endregion

        #region Properties
        public override Type VisualType => typeof(Button);
        #endregion

        #region Methods
        public IButtonElement OnClick(RoutedEventHandler handler)
        {
            AddRoutedEventHandler(Button.ClickEvent, handler);
            return this;
        }
        #endregion
    }
}
