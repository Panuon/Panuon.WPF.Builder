using System;
using System.Collections.Generic;
using System.Windows.Shapes;

namespace Panuon.WPF.Builder.Elements
{
    internal class PathElement
        : DecoratorElement, IPathElement
    {
        #region Ctor
        internal PathElement(IAppBuilder appBuilder, IDictionary<string, object> config)
            : base(appBuilder, config)
        {
        }
        #endregion

        #region Properties
        public object Data
        {
            get => GetValue(Path.DataProperty);
            set => SetValue(Path.DataProperty, value);
        }

        public override Type VisualType => typeof(Path);

        #endregion

        #region Overrides
        public override void SetValue(string propertyNameOrKey, object value)
        {
            switch (propertyNameOrKey)
            {
                case "data":
                    SetValue(Path.DataProperty, value);
                    break;
                default:
                    base.SetValue(propertyNameOrKey, value);
                    break;
            }
        }
        #endregion
    }
}
