using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Panuon.WPF.Builder.Elements
{
    internal class ListBoxElement
        : SelectorElement, IListBoxElement
    {
        #region Ctor
        internal ListBoxElement(IDictionary<string, object> config)
            : base(config)
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

            var listBox = Visual as ListBox;
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
