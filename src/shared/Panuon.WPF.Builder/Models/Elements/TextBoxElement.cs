using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Panuon.WPF.Builder.Elements
{
    internal class TextBoxElement
        : ContentControlElement, ITextBoxElement
    {
        #region Ctor
        internal TextBoxElement(IAppBuilder appBuilder, IDictionary<string, object> config)
            : base(appBuilder, config)
        {
        }
        #endregion

        #region Properties
        public object Text
        {
            get => GetValue(TextBox.TextProperty);
            set => SetValue(TextBox.TextProperty, value);
        }

        public override Type VisualType => typeof(TextBox);
        #endregion

        #region Methods
        public override void SetValue(string propertyNameOrKey, object value)
        {
            switch (propertyNameOrKey)
            {
                case "wrap":
                    SetValue(TextBox.TextWrappingProperty, value);
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
