using Panuon.WPF.Builder.Elements;
using Panuon.WPF.Builder.Utils;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Panuon.WPF.Builder
{
    public static class CheckBoxExtension
    {
        public static ICheckBoxElement CreateCheckBox(this IAppBuilder appBuilder,
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

            return new CheckBoxElement(appBuilder, config);
        }


        public static ICheckBoxElement OnClick(this ICheckBoxElement element,
            RoutedEventHandler handler)
        {
            return element.AddHandle(CheckBox.ClickEvent, handler);
        }

        public static ICheckBoxElement OnCheckChanged(this ICheckBoxElement element,
           RoutedEventHandler handler)
        {
            element.AddHandle(CheckBox.CheckedEvent, handler);
            element.AddHandle(CheckBox.UncheckedEvent, handler);
            return element.AddHandle(CheckBox.IndeterminateEvent, handler);
        }

        public static ICheckBoxElement OnChecked(this ICheckBoxElement element,
            RoutedEventHandler handler)
        {
            return element.AddHandle(CheckBox.CheckedEvent, handler);
        }

        public static ICheckBoxElement OnUnchecked(this ICheckBoxElement element,
            RoutedEventHandler handler)
        {
            return element.AddHandle(CheckBox.UncheckedEvent, handler);
        }

        public static ICheckBoxElement OnIndeterminate(this ICheckBoxElement element,
           RoutedEventHandler handler)
        {
            return element.AddHandle(CheckBox.IndeterminateEvent, handler);
        }

        public static ICheckBoxElement OnIsCheckedChanged(this ICheckBoxElement element,
            RoutedEventHandler handler)
        {
            element.AddHandle(CheckBox.CheckedEvent, handler);
            element.AddHandle(CheckBox.IndeterminateEvent, handler);
            return element.AddHandle(CheckBox.UncheckedEvent, handler);
        }

        public static ICheckBoxElement OnDoubleClick(this ICheckBoxElement element,
            MouseButtonEventHandler handler)
        {
            return element.AddHandle(CheckBox.MouseDoubleClickEvent, handler);
        }
    }
}
