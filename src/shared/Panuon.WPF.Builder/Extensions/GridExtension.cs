using Panuon.WPF.Builder.Elements;
using Panuon.WPF.Builder.Utils;
using System.Collections.Generic;
using System.Reflection;

namespace Panuon.WPF.Builder
{
    public static class GridExtension
    {
        #region Methods
        public static IGridElement CreateGrid(this IAppBuilder appBuilder,
            IEnumerable<IModule> children = null,
            IEnumerable<object> rows = null,
            IEnumerable<object> columns = null,
            object visibility = null,
            object width = null, object height = null,
            object minWidth = null, object minHeight = null,
            object maxWidth = null, object maxHeight = null,
            object margin = null, object padding = null,
            object horizontal = null, object vertical = null,
            object horizontalSeparatorBrush = null, object verticalSeparatorBrush = null)
        {
            var config = ParameterUtil.GetParameters(MethodBase.GetCurrentMethod(),
                appBuilder,
                children,
                rows, columns,
                visibility,
                width, height,
                minWidth, minHeight,
                maxWidth, maxHeight,
                margin, padding,
                horizontal, vertical,
                horizontalSeparatorBrush, verticalSeparatorBrush);

            return new GridElement(appBuilder, config);
        }
        #endregion
    }
}
