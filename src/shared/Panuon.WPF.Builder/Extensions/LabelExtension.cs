using Panuon.WPF.Builder.Elements;
using Panuon.WPF.Builder.Utils;
using System.Reflection;

namespace Panuon.WPF.Builder
{
    public static class LabelExtension
    {
        public static ILabelElement CreateLabel(this IAppBuilder appBuilder,
            object content = null,
            object style = null,
            object visibility = null,
            object width = null, object height = null,
            object minWidth = null, object minHeight = null,
            object maxWidth = null, object maxHeight = null,
            object margin = null, object padding = null,
            object horizontal = null, object vertical = null,
            object contentHorizontal = null, object contentVertical = null,
            object fontSize = null, object fontFamily = null, object fontWeight = null, object fontStyle = null, object fontStretch = null)
        {
            var config = ParameterUtil.GetParameters(MethodBase.GetCurrentMethod(),
                appBuilder,
                content,
                style,
                visibility,
                width, height,
                minWidth, minHeight,
                maxWidth, maxHeight,
                margin, padding,
                horizontal, vertical,
                contentHorizontal, contentVertical,
                fontSize, fontFamily, fontWeight, fontStyle, fontStretch);

            return new LabelElement(config);
        }
    }
}
