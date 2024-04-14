using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Panuon.WPF.Builder.Elements
{
    internal class TextBoxElement
        : ContentControlElement, ITextBoxElement
    {
        #region Ctor
        internal TextBoxElement(IDictionary<string, object> config)
            : base(config)
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

        protected override bool SetPropertyValue(string propertyKey,
            object value)
        {
            switch (propertyKey)
            {
                case "wrap":
                    SetValue(TextBox.TextWrappingProperty, value);
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
