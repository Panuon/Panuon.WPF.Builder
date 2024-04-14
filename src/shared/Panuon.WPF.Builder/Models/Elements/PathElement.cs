using System;
using System.Collections.Generic;
using System.Windows.Shapes;

namespace Panuon.WPF.Builder.Elements
{
    internal class PathElement
        : DecoratorElement, IPathElement
    {
        #region Ctor
        internal PathElement(IDictionary<string, object> config)
            : base(config)
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

        protected override bool SetPropertyValue(string propertyKey,
            object value)
        {
            switch (propertyKey)
            {
                case "data":
                    SetValue(Path.DataProperty, value);
                    return true;
            }

            return base.SetPropertyValue(propertyKey, value);
        }
        #endregion
    }
}
