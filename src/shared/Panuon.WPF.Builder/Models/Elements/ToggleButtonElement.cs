using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Panuon.WPF.Builder.Elements
{
    internal class ToggleButtonElement
        : ContentControlElement, IToggleButtonElement
    {
        #region Ctor
        internal ToggleButtonElement(IDictionary<string, object> config)
            : base(config)
        {
        }
        #endregion

        #region Properties
        public override Type VisualType => typeof(ToggleButton);
        #endregion

        #region Methods
        protected override bool SetPropertyValue(string propertyKey,
            object value)
        {
            switch (propertyKey)
            {
                case "ischecked":
                    SetValue(ToggleButton.IsCheckedProperty, value);
                    return true;
                case "text":
                    SetValue(TextBox.TextProperty, value);
                    return true;
            }

            return base.SetPropertyValue(propertyKey, value);
        }
        #endregion
    }
}
