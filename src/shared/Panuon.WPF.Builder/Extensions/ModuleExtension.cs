using Panuon.WPF.Builder.Elements;
using System.Windows;

namespace Panuon.WPF.Builder
{
    public static class ModuleExtension
    {
        public static TModule Set<TModule>(this TModule module,
            string propertyNameOrKey,
            object value,
            bool updateActualVisual = false)
            where TModule : IModule
        {
            module.SetValue(propertyNameOrKey, value, updateActualVisual);
            return module;
        }

        public static TModule Set<TModule>(this TModule module,
            DependencyProperty property,
            object value,
            bool updateActualVisual = false)
            where TModule : IModule
        {
            module.SetValue(property, value, updateActualVisual);
            return module;
        }

        public static TModule SetWidth<TModule>(this TModule module,
            object width)
            where TModule : IModule
        {
            module.SetValue(FrameworkElement.WidthProperty, width);
            return module;
        }

        public static TModule SetHeight<TModule>(this TModule module,
           object height)
           where TModule : IModule
        {
            module.SetValue(FrameworkElement.HeightProperty, height);
            return module;
        }

        public static TModule SetMargin<TModule>(this TModule module,
            object margin)
            where TModule : IModule
        {
            module.SetValue(FrameworkElement.MarginProperty, margin);
            return module;
        }

        public static TModule SetHorizontal<TModule>(this TModule module,
            object horizontal)
            where TModule : IModule
        {
            module.SetValue(FrameworkElement.HorizontalAlignmentProperty, horizontal);
            return module;
        }

        public static TModule SetVertical<TModule>(this TModule module,
            object vertical)
            where TModule : IModule
        {
            module.SetValue(FrameworkElement.HorizontalAlignmentProperty, vertical);
            return module;
        }

        public static TModule SetGridRow<TModule>(this TModule module,
            object row)
            where TModule : IModule
        {
            GridElement.SetRow(module, row);
            return module;
        }

        public static TModule SetGridRowSpan<TModule>(this TModule module,
            object rowSpan)
            where TModule : IModule
        {
            GridElement.SetRowSpan(module, rowSpan);
            return module;
        }

        public static TModule SetGridColumn<TModule>(this TModule module,
            object column)
            where TModule : IModule
        {
            GridElement.SetColumn(module, column);
            return module;
        }

        public static TModule SetGridColumnSpan<TModule>(this TModule module,
            object columnSpan)
            where TModule : IModule
        {
            GridElement.SetColumnSpan(module, columnSpan);
            return module;
        }

        public static TModule SetVisibility<TModule>(this TModule module,
            object visibility)
            where TModule : IModule
        {
            module.SetValue(FrameworkElement.VisibilityProperty, visibility);
            return module;
        }
    }
}
