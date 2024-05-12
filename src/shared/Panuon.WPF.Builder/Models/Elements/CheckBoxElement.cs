using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Panuon.WPF.Builder.Elements
{
    internal class CheckBoxElement
        : ToggleButtonElement, ICheckBoxElement
    {
        #region Ctor
        internal CheckBoxElement(IAppBuilder appBuilder, IDictionary<string, object> config)
            : base(appBuilder, config)
        {
        }
        #endregion

        #region Properties
        public override Type VisualType => typeof(CheckBox);
        #endregion

        #region Methods
        #endregion
    }
}
