using Panuon.WPF.Builder.Elements;
using Panuon.WPF.Builder.Utils;
using System.Reflection;

namespace Panuon.WPF.Builder
{
    public static class PathExtension
    {
        public static IPathElement CreatePath(this IAppBuilder appBuilder,
            object data = null,
            object style = null,
            object visibility = null,
            object width = null, object height = null,
            object minWidth = null, object minHeight = null,
            object maxWidth = null, object maxHeight = null,
            object margin = null,
            object horizontal = null, object vertical = null,
            object fill = null,
            object stroke = null, object strokeThickness = null, object strokeDashArray = null, object strokeDashCap = null, object strokeDashOffset = null)
        {
            var config = ParameterUtil.GetParameters(MethodBase.GetCurrentMethod(),
                appBuilder,
                data,
                style,
                visibility,
                width, height,
                minWidth, minHeight,
                maxWidth, maxHeight,
                margin,
                horizontal, vertical,
                fill,
                stroke, strokeThickness, strokeDashArray, strokeDashCap, strokeDashOffset);

            return new PathElement(config);
        }
    }
}
