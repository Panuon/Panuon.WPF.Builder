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
        internal SelectorElement(IAppBuilder appBuilder, IDictionary<string, object> config)
            : base(appBuilder, config)
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

        public object SelectedValue
        {
            get => GetValue(Selector.SelectedValueProperty);
            set => SetValue(Selector.SelectedValueProperty, value);
        }
        #endregion

        #region Methods

        public ISelectorElement OnSelectionChanged(SelectionChangedEventHandler handler)
        {
            AddRoutedEventHandler(Selector.SelectionChangedEvent, handler);
            return this;
        }

        public override void SetValue(string propertyNameOrKey, object value)
        {
            switch (propertyNameOrKey)
            {
                case "selectedvaluepath":
                    SetValue(Selector.SelectedValuePathProperty, value);
                    break;
                case "selectedindex":
                    SetValue(Selector.SelectedIndexProperty, value);
                    break;
                case "selecteditem":
                    SetValue(Selector.SelectedItemProperty, value);
                    break;
                case "selectedvalue":
                    SetValue(Selector.SelectedValueProperty, value);
                    break;
                default:
                    base.SetValue(propertyNameOrKey, value);
                    break;
            }
        }
        #endregion
    }
}
