using System;
using System.Windows;
using System.Windows.Controls;

namespace Panuon.WPF.Builder
{
    internal sealed class TemplateGeneratorControl 
        : ContentControl
    {
        internal static readonly DependencyProperty FactoryProperty
            = DependencyProperty.Register("Factory", typeof(Func<object>), typeof(TemplateGeneratorControl), new PropertyMetadata(null, FactoryChanged));

        private static void FactoryChanged(DependencyObject instance, DependencyPropertyChangedEventArgs args)
        {
            var control = (TemplateGeneratorControl)instance;
            var factory = (Func<object>)args.NewValue;
            control.Content = factory.Invoke();
        }
    }
}
