using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Panuon.WPF.Builder.Elements
{
    internal class BorderElement
        : DecoratorElement, IBorderElement
    {
        #region Ctor
        internal BorderElement(Dictionary<string, object> config)
            : base(config)
        {
        }
        #endregion

        #region Properties
        public override Type VisualType => typeof(Border);
        #endregion

        #region Overrides
        #endregion
    }
}
