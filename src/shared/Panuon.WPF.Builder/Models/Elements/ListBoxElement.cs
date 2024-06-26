﻿using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Panuon.WPF.Builder.Elements
{
    internal class ListBoxElement
        : SelectorElement, IListBoxElement
    {
        #region Ctor
        internal ListBoxElement(IAppBuilder appBuilder, IDictionary<string, object> config)
            : base(appBuilder, config)
        {
        }
        #endregion

        #region Properties
        public override Type VisualType => typeof(ListBox);
        #endregion

        #region Overrids
        protected override void OnInitializing()
        {
            base.OnInitializing();
        }
        #endregion

        #region Methods
        public new IListBoxElement OnSelectionChanged(SelectionChangedEventHandler handler)
        {
            return (IListBoxElement)base.OnSelectionChanged(handler);
        }
        #endregion
    }

}
