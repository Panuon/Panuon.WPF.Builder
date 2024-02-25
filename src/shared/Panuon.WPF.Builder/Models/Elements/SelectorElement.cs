using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
namespace Panuon.WPF.Builder
{
    internal abstract class SelectorElement
        : ItemsControlElement, ISelectorElement
    {
        #region Ctor
        internal SelectorElement(IDictionary<string, object> config)
            : base(config)
        {
        }
        #endregion

        #region Properties
        public override Type VisualType => typeof(Selector);

        public object SelectedItem
        {
            get => GetValue(Selector.SelectedItemProperty);
            set => SetValue(Selector.SelectedItemProperty, value);
        }

        public int SelectedIndex
        {
            get => (int)GetValue(Selector.SelectedIndexProperty);
            set => SetValue(Selector.SelectedIndexProperty, value);
        }
        #endregion

        #region Methods

        public ISelectorElement OnSelectionChanged(SelectionChangedEventHandler handler)
        {
            AddRoutedEventHandler(Selector.SelectionChangedEvent, handler);
            return this;
        }

        protected override bool SetPropertyValue(string propertyKey,
            object value)
        {
            switch (propertyKey)
            {
                case "selectedvaluepath":
                    SetValue(Selector.SelectedValuePathProperty, value);
                    return true;
                case "selectedindex":
                    SetValue(Selector.SelectedIndexProperty, value);
                    return true;
                case "selecteditem":
                    SetValue(Selector.SelectedItemProperty, value);
                    return true;
            }

            return base.SetPropertyValue(propertyKey, value);
        }
        #endregion
    }
}
