using Panuon.WPF.Builder.Elements;
using Panuon.WPF.Builder.Utils;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Panuon.WPF.Builder
{
    public static class ToggleButtonExtension
    {
        public static IToggleButtonElement CreateToggleButton(this IAppBuilder appBuilder,
            object content = null,
            object isChecked = null,
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
                isChecked,
                style,
                visibility,
                width, height,
                minWidth, minHeight,
                maxWidth, maxHeight,
                margin, padding,
                horizontal, vertical,
                contentHorizontal, contentVertical,
                fontSize, fontFamily, fontWeight, fontStyle, fontStretch);

            return new ToggleButtonElement(config);
        }


        public static IToggleButtonElement OnClick(this IToggleButtonElement element,
            RoutedEventHandler handler)
        {
            return element.AddHandle(ToggleButton.ClickEvent, handler);
        }

        public static IToggleButtonElement OnChecked(this IToggleButtonElement element,
            RoutedEventHandler handler)
        {
            return element.AddHandle(ToggleButton.CheckedEvent, handler);
        }

        public static IToggleButtonElement OnUnchecked(this IToggleButtonElement element,
            RoutedEventHandler handler)
        {
            return element.AddHandle(ToggleButton.UncheckedEvent, handler);
        }

        public static IToggleButtonElement OnIndeterminate(this IToggleButtonElement element,
           RoutedEventHandler handler)
        {
            return element.AddHandle(ToggleButton.IndeterminateEvent, handler);
        }

        public static IToggleButtonElement OnIsCheckedChanged(this IToggleButtonElement element,
            RoutedEventHandler handler)
        {
            element.AddHandle(ToggleButton.CheckedEvent, handler);
            element.AddHandle(ToggleButton.IndeterminateEvent, handler);
            return element.AddHandle(ToggleButton.UncheckedEvent, handler);
        }

        public static IToggleButtonElement OnDoubleClick(this IToggleButtonElement element,
            MouseButtonEventHandler handler)
        {
            return element.AddHandle(ToggleButton.MouseDoubleClickEvent, handler);
        }
    }
}
