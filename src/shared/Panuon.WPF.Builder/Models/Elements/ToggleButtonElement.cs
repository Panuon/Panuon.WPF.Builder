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
        internal ToggleButtonElement(IAppBuilder appBuilder, IDictionary<string, object> config)
            : base(appBuilder, config)
        {
        }
        #endregion

        #region Properties
        public override Type VisualType => typeof(ToggleButton);
        #endregion

        #region Methods
        public override void SetValue(string propertyNameOrKey, object value)
        {
            switch (propertyNameOrKey)
            {
                case "ischecked":
                    SetValue(ToggleButton.IsCheckedProperty, value);
                    break;
                case "text":
                    SetValue(TextBox.TextProperty, value);
                    break;
                default:
                    base.SetValue(propertyNameOrKey, value);
                    break;
            }
        }
        #endregion
    }
}
