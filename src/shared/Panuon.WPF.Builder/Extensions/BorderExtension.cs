using Panuon.WPF.Builder.Elements;
using Panuon.WPF.Builder.Utils;
using System.Reflection;

namespace Panuon.WPF.Builder
{
    public static class BorderExtension
    {
        public static IBorderElement CreateBorder(this IAppBuilder appBuilder,
            object content = null,
            object style = null,
            object visibility = null,
            object width = null, object height = null,
            object minWidth = null, object minHeight = null,
            object maxWidth = null, object maxHeight = null,
            object margin = null, object padding = null,
            object horizontal = null, object vertical = null,
            object contentHorizontal = null, object contentVertical = null)
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
                contentHorizontal, contentVertical);

            return new BorderElement(appBuilder, config);
        }

        public static IBorderElement CustomStyle(this IBorderElement element,
            object background = null,
            object borderBrush = null,
            object borderThickness = null,
            object cornerRadius = null)
        {
            var config = ParameterUtil.GetParameters(MethodBase.GetCurrentMethod(),
                element,
                background, borderBrush, borderThickness, cornerRadius);

            foreach (var keyValue in config)
            {
                if (keyValue.Value != null)
                {
                    element.Set(keyValue.Key, keyValue.Value);
                }
            }
            return element;
        }

        public static IBorderElement AddBorder(this IModule module,
            object content = null,
            object style = null,
            object visibility = null,
            object width = null, object height = null,
            object minWidth = null, object minHeight = null,
            object maxWidth = null, object maxHeight = null,
            object margin = null, object padding = null,
            object horizontal = null, object vertical = null,
            object contentHorizontal = null, object contentVertical = null)
        {
            var border = module.AppBuilder.CreateBorder(
                content: content,
                style: style,
                visibility: visibility,
                width: width, height: height,
                minWidth: minWidth, minHeight: minHeight,
                maxWidth: maxWidth, maxHeight: maxHeight,
                margin: margin, padding: padding,
                horizontal: horizontal, vertical: vertical,
                contentHorizontal: contentHorizontal, contentVertical: contentVertical);

            border.Child = module;
            return border;
        }

        public static IScrollViewerElement AddScrollViewer(this IModule module,
            object content = null,
            object style = null,
            object visibility = null,
            object width = null, object height = null,
            object minWidth = null, object minHeight = null,
            object maxWidth = null, object maxHeight = null,
            object margin = null, object padding = null,
            object horizontal = null, object vertical = null,
            object contentHorizontal = null, object contentVertical = null)
        {
            var scrollViewer = module.AppBuilder.CreateScrollViewer(
                content: content,
                style: style,
                visibility: visibility,
                width: width, height: height,
                minWidth: minWidth, minHeight: minHeight,
                maxWidth: maxWidth, maxHeight: maxHeight,
                margin: margin, padding: padding,
                horizontal: horizontal, vertical: vertical,
                contentHorizontal: contentHorizontal, contentVertical: contentVertical);

            scrollViewer.Content = module;
            return scrollViewer;
        }
    }
}
