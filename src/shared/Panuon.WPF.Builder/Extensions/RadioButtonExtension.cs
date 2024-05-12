using Panuon.WPF.Builder.Elements;
using Panuon.WPF.Builder.Utils;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Panuon.WPF.Builder
{
    public static class RadioButtonExtension
    {
        public static IRadioButtonElement CreateRadioButton(this IAppBuilder appBuilder,
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

            return new RadioButtonElement(appBuilder, config);
        }


        public static IRadioButtonElement OnClick(this IRadioButtonElement element,
            RoutedEventHandler handler)
        {
            return element.AddHandle(RadioButton.ClickEvent, handler);
        }

        public static IRadioButtonElement OnCheckChanged(this IRadioButtonElement element,
           RoutedEventHandler handler)
        {
            element.AddHandle(RadioButton.CheckedEvent, handler);
            element.AddHandle(RadioButton.UncheckedEvent, handler);
            return element.AddHandle(RadioButton.IndeterminateEvent, handler);
        }

        public static IRadioButtonElement OnChecked(this IRadioButtonElement element,
            RoutedEventHandler handler)
        {
            return element.AddHandle(RadioButton.CheckedEvent, handler);
        }

        public static IRadioButtonElement OnUnchecked(this IRadioButtonElement element,
            RoutedEventHandler handler)
        {
            return element.AddHandle(RadioButton.UncheckedEvent, handler);
        }

        public static IRadioButtonElement OnIndeterminate(this IRadioButtonElement element,
           RoutedEventHandler handler)
        {
            return element.AddHandle(RadioButton.IndeterminateEvent, handler);
        }

        public static IRadioButtonElement OnIsCheckedChanged(this IRadioButtonElement element,
            RoutedEventHandler handler)
        {
            element.AddHandle(RadioButton.CheckedEvent, handler);
            element.AddHandle(RadioButton.IndeterminateEvent, handler);
            return element.AddHandle(RadioButton.UncheckedEvent, handler);
        }

        public static IRadioButtonElement OnDoubleClick(this IRadioButtonElement element,
            MouseButtonEventHandler handler)
        {
            return element.AddHandle(RadioButton.MouseDoubleClickEvent, handler);
        }
    }
}
