using Panuon.WPF.Builder.Elements;
using System.Windows;

namespace Panuon.WPF.Builder
{
    public static class ModuleExtension
    {
        public static TModule Set<TModule>(this TModule module,
            string propertyNameOrKey,
            object value)
            where TModule : IModule
        {
            module.SetValue(propertyNameOrKey, value);
            return module;
        }

        public static TModule Set<TModule>(this TModule module,
            DependencyProperty property,
            object value)
            where TModule : IModule
        {
            module.SetValue(property, value);
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

        public static TModule SetMargin<TModule>(this TModule module,
           double left,
           double top,
           double right,
           double bottom)
           where TModule : IModule
        {
            module.SetValue(FrameworkElement.MarginProperty, new Thickness(left, top, right, bottom));
            return module;
        }

        #region Align
        public static TModule AlignLeft<TModule>(this TModule module)
            where TModule : IModule
        {
            module.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Left);
            return module;
        }

        public static TModule AlignRight<TModule>(this TModule module,
            object horizontal)
            where TModule : IModule
        {
            module.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Right);
            return module;
        }

        public static TModule AlignTop<TModule>(this TModule module)
            where TModule : IModule
        {
            module.SetValue(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Top);
            return module;
        }

        public static TModule AlignBottom<TModule>(this TModule module)
           where TModule : IModule
        {
            module.SetValue(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Bottom);
            return module;
        }

        public static TModule AlignCenter<TModule>(this TModule module)
           where TModule : IModule
        {
            module.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            module.SetValue(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Center);
            return module;
        }

        public static TModule HorizontalAlignCenter<TModule>(this TModule module)
           where TModule : IModule
        {
            module.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            return module;
        }

        public static TModule VerticalAlignCenter<TModule>(this TModule module)
          where TModule : IModule
        {
            module.SetValue(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Center);
            return module;
        }
        #endregion

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
