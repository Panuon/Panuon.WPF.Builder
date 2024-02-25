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
        internal ItemsControlElement(IDictionary<string, object> config)
            : base(config)
        {
        }
        #endregion

        #region Properties
        public override Type VisualType => typeof(ItemsControl);
        #endregion

        #region Methods
        protected override bool SetPropertyValue(string propertyKey,
            object value)
        {
            switch (propertyKey)
            {
                case "itemssource":
                    SetValue(ItemsControl.ItemsSourceProperty, value);
                    return true;
                case "itemtemplate":
                    SetValue(ItemsControl.ItemTemplateProperty, value);
                    return true;
                case "itemspanel":
                    SetValue(ItemsControl.ItemsPanelProperty, value);
                    return true;
                case "displaymemberpath":
                    SetValue(ItemsControl.DisplayMemberPathProperty, value);
                    return true;
            }

            return base.SetPropertyValue(propertyKey, value);
        }
        #endregion
    }
}
