using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Panuon.WPF.Builder.Elements
{
    internal class ComboBoxElement
        : SelectorElement, IComboBoxElement
    {
        #region Ctor
        internal ComboBoxElement(IAppBuilder appBuilder,
            IDictionary<string, object> config)
            : base(appBuilder, config)
        {
        }
        #endregion

        #region Properties
        public override Type VisualType => typeof(ComboBox);
        #endregion

        #region Overrids
        protected override void OnInitializing()
        {
            base.OnInitializing();
        }
        #endregion

        #region Methods
        public new IComboBoxElement OnSelectionChanged(SelectionChangedEventHandler handler)
        {
            return (IComboBoxElement)base.OnSelectionChanged(handler);
        }
        #endregion
    }

}
