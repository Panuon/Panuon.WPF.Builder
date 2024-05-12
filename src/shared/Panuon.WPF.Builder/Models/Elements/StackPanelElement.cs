using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Panuon.WPF.Builder.Elements
{
    internal class StackPanelElement
        : PanelElement, IStackPanelElement
    {
        #region Ctor
        internal StackPanelElement(IAppBuilder appBuilder, IDictionary<string, object> config)
            : base(appBuilder, config)
        {
        }
        #endregion

        #region Properties
        public override Type VisualType => typeof(StackPanel);
        #endregion

        #region Methods
        #endregion

        #region Overrides
        public override void SetValue(string propertyNameOrKey, object value)
        {
            switch (propertyNameOrKey)
            {
                case "orientation":
                    SetValue(StackPanel.OrientationProperty, value);
                    break;
                default:
                    base.SetValue(propertyNameOrKey, value);
                    break;
            }
        }
        #endregion

        #region Functions
        #endregion
    }
}
