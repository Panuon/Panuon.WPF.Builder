using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Panuon.WPF.Builder.Elements
{
    internal class ScrollViewerElement
        : ContentControlElement, IScrollViewerElement
    {
        #region Ctor
        internal ScrollViewerElement(IAppBuilder appBuilder,
            IDictionary<string, object> config)
            : base(appBuilder, config)
        {
        }
        #endregion

        #region Properties
        public override Type VisualType => typeof(ScrollViewer);
        #endregion

        #region Methods
        protected override void OnInitializing()
        {
            base.OnInitializing();
        }
        #endregion
    }
}
