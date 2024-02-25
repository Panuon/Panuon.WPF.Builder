using Panuon.WPF.Builder.Elements;
using Panuon.WPF.Builder.Utils;
using System.Reflection;

namespace Panuon.WPF.Builder
{
    public static class TextBlockExtension
    {
        public static ITextBlockElement CreateTextBlock(this IAppBuilder appBuilder,
            object text = null,
            object textWrapping = null,
            object style = null,
            object foreground = null,
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
                text,
                textWrapping,
                style,
                foreground,
                visibility,
                width, height,
                minWidth, minHeight,
                maxWidth, maxHeight,
                margin, padding,
                horizontal, vertical,
                contentHorizontal, contentVertical,
                fontSize, fontFamily, fontWeight, fontStyle, fontStretch);
            return new TextBlockElement(config);
        }
    }
}
