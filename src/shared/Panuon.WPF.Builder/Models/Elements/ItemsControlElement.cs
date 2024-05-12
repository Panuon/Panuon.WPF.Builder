using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Panuon.WPF.Builder
{
    public class ItemsControlElement
        : Element, IItemsControlElement
    {
        #region Fields
        #endregion

        #region Ctor
        internal ItemsControlElement(IAppBuilder appBuilder, IDictionary<string, object> config)
            : base(appBuilder, config)
        {
        }
        #endregion

        #region Properties
        public override Type VisualType => typeof(ItemsControl);
        #endregion

        #region Methods
        public override void SetValue(string propertyNameOrKey, object value)
        {
            switch (propertyNameOrKey)
            {
                case "itemssource":
                    SetValue(ItemsControl.ItemsSourceProperty, value);
                    break;
                case "itemtemplate":
                    SetValue(ItemsControl.ItemTemplateProperty, value);
                    break;
                case "itemspanel":
                    SetValue(ItemsControl.ItemsPanelProperty, value);
                    break;
                case "displaymemberpath":
                    SetValue(ItemsControl.DisplayMemberPathProperty, value);
                    break;
                default:
                    base.SetValue(propertyNameOrKey, value);
                    break;
            }
        }
        #endregion
    }
}
