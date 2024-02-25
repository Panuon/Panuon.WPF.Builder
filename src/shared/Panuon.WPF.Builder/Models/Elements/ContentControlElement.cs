using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Panuon.WPF.Builder.Elements
{
    internal class ContentControlElement
        : Element, IContentControlElement
    {
        #region Ctor
        internal ContentControlElement(IDictionary<string, object> config)
            : base(config)
        {
        }
        #endregion

        #region Properties
        public object Content
        {
            get => GetValue(ContentControl.ContentProperty);
            set
            {
                if (value is Module module)
                {
                    SetValue(ContentControl.ContentProperty, module.ActualVisual);
                }
                else
                {
                    SetValue(ContentControl.ContentProperty, value);
                }
            }
        }

        public override Type VisualType => typeof(ContentControl);
        #endregion

        #region Overrids
        protected override bool SetPropertyValue(string propertyKey,
            object value)
        {
            switch (propertyKey)
            {
                case "content":
                    if (value is Module module)
                    {
                        SetValue(ContentControl.ContentProperty, module.ActualVisual);
                    }
                    else
                    {
                        SetValue(ContentControl.ContentProperty, value);
                    }
                    return true;
                case "contenttemplate":
                    SetValue(ContentControl.ContentTemplateProperty, value);
                    return true;
            }

            return base.SetPropertyValue(propertyKey, value);
        }
        #endregion
    }
}