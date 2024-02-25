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
            IEnumerable<object> rows = null,
            IEnumerable<object> columns = null,
            bool autoAllocate = true,
            object width = null,
            object height = null,
            object horizontal = null,
            object vertical = null,
            object background = null,
            object borderBrush = null,
            object borderThickness = null,
            object margin = null,
            object padding = null,
            object horizontalSeparatorBrush = null,
            object verticalSeparatorBrush = null)
        {
            var config = ParameterUtil.GetParameters(MethodBase.GetCurrentMethod(),
                appBuilder,
                children,
                rows, columns,
                autoAllocate,
                width, height,
                vertical, horizontal,
                background, borderBrush, borderThickness, 
                margin, padding,
                horizontalSeparatorBrush, verticalSeparatorBrush);

            return new StackPanelElement(config);
        }
        #endregion
    }
}
