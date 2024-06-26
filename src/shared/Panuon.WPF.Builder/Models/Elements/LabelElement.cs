﻿using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Panuon.WPF.Builder.Elements
{
    internal class LabelElement
        : ContentControlElement, ILabelElement
    {
        #region Ctor
        internal LabelElement(IAppBuilder appBuilder, 
            IDictionary<string, object> config)
            : base(appBuilder, config)
        {
        }
        #endregion

        #region Properties
        public override Type VisualType => typeof(Label);
        #endregion

        #region Methods
        protected override void OnInitializing()
        {
            base.OnInitializing();
        }
        #endregion
    }
}
