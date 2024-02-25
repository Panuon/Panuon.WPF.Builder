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
            return new Border()
            {
                Child = element,
            };
        }
        #endregion

        #region Overrides
        
        #endregion

        #region Functions
        #endregion
    }
}
