using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Panuon.WPF.Builder.Elements
{
    internal class BorderElement
        : DecoratorElement, IBorderElement
    {
        #region Ctor
        internal BorderElement(IAppBuilder appBuilder, IDictionary<string, object> config)
            : base(appBuilder, config)
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
