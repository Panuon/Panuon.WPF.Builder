﻿using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Panuon.WPF.Builder.Elements
{
    internal class RadioButtonElement
        : ToggleButtonElement, IRadioButtonElement
    {
        #region Ctor
        internal RadioButtonElement(IAppBuilder appBuilder, IDictionary<string, object> config)
            : base(appBuilder, config)
        {
        }
        #endregion

        #region Properties
        public override Type VisualType => typeof(RadioButton);
        #endregion

        #region Methods
        #endregion
    }
}
