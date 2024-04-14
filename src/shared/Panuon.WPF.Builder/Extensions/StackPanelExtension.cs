using Panuon.WPF.Builder.Elements;
using Panuon.WPF.Builder.Utils;
using System.Collections.Generic;
using System.Reflection;

namespace Panuon.WPF.Builder
{
    public static class StackPanelExtension
    {
        #region Methods
        public static IStackPanelElement CreateStackPanel(this IAppBuilder appBuilder,
            IEnumerable<IModule> children,
            object orientation = null,
            bool canHorizontalScroll = false, bool canVerticalScroll = false,
            object visibility = null, object isEnabled = null,
            object width = null, object height = null,
            object minWidth = null, object minHeight = null,
            object maxWidth = null, object maxHeight = null,
            object margin = null, object padding = null,
            object horizontal = null, object vertical = null)
        {
            var config = ParameterUtil.GetParameters(MethodBase.GetCurrentMethod(),
                appBuilder,
                children,
                orientation,
                canHorizontalScroll, canVerticalScroll,
                visibility, isEnabled,
                width, height,
                minWidth, minHeight,
                maxWidth, maxHeight,
                margin, padding,
                horizontal, vertical);

            return new StackPanelElement(config);
        }
        #endregion
    }
}
