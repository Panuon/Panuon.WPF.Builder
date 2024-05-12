using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Panuon.WPF.Builder.Elements
{
    internal class ContentControlElement
        : Element, IContentControlElement
    {
        #region Ctor
        internal ContentControlElement(IAppBuilder appBuilder, IDictionary<string, object> config)
            : base(appBuilder, config)
        {
        }
        #endregion

        #region Properties
        public object Content
        {
            get => GetValue(ContentControl.ContentProperty);
            set
            {
                SetValue(ContentControl.ContentProperty, value);
            }
        }

        public override Type VisualType => typeof(ContentControl);
        #endregion

        #region Overrids
        public override void SetValue(string propertyNameOrKey, object value)
        {
            switch (propertyNameOrKey)
            {
                case "content":
                    SetValue(ContentControl.ContentProperty, value);
                    break;
                case "contenttemplate":
                    SetValue(ContentControl.ContentTemplateProperty, value);
                    break;
                default:
                    base.SetValue(propertyNameOrKey, value);
                    break;
            }
        }
        #endregion
    }
}