using Panuon.WPF.Builder.Elements;
using Panuon.WPF.Builder.Utils;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Panuon.WPF.Builder
{
    public static class PasswordBoxExtension
    {
        public static IPasswordBoxElement CreatePasswordBox(this IAppBuilder appBuilder,
            object password = null,
            object textWrapping = null,
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
                password,
                textWrapping,
                style,
                visibility,
                width, height,
                minWidth, minHeight,
                maxWidth, maxHeight,
                margin, padding,
                horizontal, vertical,
                contentHorizontal, contentVertical,
                fontSize, fontFamily, fontWeight, fontStyle, fontStretch);

            return new PasswordBoxElement(appBuilder, config);
        }

        public static IPasswordBoxElement OnPasswordChanged(this IPasswordBoxElement element,
            RoutedEventHandler handler)
        {
            return element.AddHandle(PasswordBox.PasswordChangedEvent, handler);
        }
    }
}
