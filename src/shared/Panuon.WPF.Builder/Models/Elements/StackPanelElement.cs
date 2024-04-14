using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Panuon.WPF.Builder.Elements
{
    internal class StackPanelElement
        : PanelElement, IStackPanelElement
    {
        #region Ctor
        internal StackPanelElement(IDictionary<string, object> config)
            : base(config)
        {
        }
        #endregion

        #region Properties
        public override Type VisualType => typeof(StackPanel);
        #endregion

        #region Methods
        protected override FrameworkElement OnCreatingActualVisual(FrameworkElement element)
        {
            var canHorizontalScroll = GetConfig("canHorizontalScroll") as bool?;
            var canVerticalScroll = GetConfig("canVerticalScroll") as bool?;

            if (canHorizontalScroll == true || canVerticalScroll == true)
            {
                var scrollViewer = new ScrollViewer()
                {
                    HorizontalScrollBarVisibility = canHorizontalScroll == true ? ScrollBarVisibility.Auto : ScrollBarVisibility.Disabled,
                    VerticalScrollBarVisibility = canVerticalScroll == true ? ScrollBarVisibility.Auto : ScrollBarVisibility.Disabled,
                    Content = element,
                };
                return scrollViewer;
            }
            return base.OnCreatingActualVisual(element);
        }
        #endregion

        #region Overrides
        protected override bool SetPropertyValue(string propertyKey,
            object value)
        {
            switch (propertyKey)
            {
                case "orientation":
                    SetValue(StackPanel.OrientationProperty, value);
                    return true;
            }

            return base.SetPropertyValue(propertyKey, value);
        }
        #endregion

        #region Functions
        #endregion
    }
}
