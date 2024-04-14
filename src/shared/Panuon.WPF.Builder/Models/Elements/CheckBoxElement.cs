using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Panuon.WPF.Builder.Elements
{
    internal class CheckBoxElement
        : ToggleButtonElement, ICheckBoxElement
    {
        #region Ctor
        internal CheckBoxElement(IDictionary<string, object> config)
            : base(config)
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
