using System;
using System.Windows;
using System.Windows.Controls;

namespace Panuon.WPF.Builder
{
    internal sealed class ItemsPanelTemplateGeneratorControl 
        : Panel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            foreach(FrameworkElement panel in Children)
            {
                panel.Measure(availableSize);
            }
            return base.MeasureOverride(availableSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            foreach(FrameworkElement panel in Children)
            {
                panel.Arrange(new Rect(0, 0, finalSize.Width, finalSize.Height));
            }
            return base.ArrangeOverride(finalSize);
        }

        internal static readonly DependencyProperty FactoryProperty
            = DependencyProperty.Register("Factory", typeof(Func<Panel>), typeof(ItemsPanelTemplateGeneratorControl), new PropertyMetadata(null, FactoryChanged));

        private static void FactoryChanged(DependencyObject instance, DependencyPropertyChangedEventArgs args)
        {
            var control = (ItemsPanelTemplateGeneratorControl)instance;
            var factory = (Func<Panel>)args.NewValue;
            var panel = factory.Invoke();
            panel.IsItemsHost = true;
            control.Children.Add(panel);
        }
    }
}
