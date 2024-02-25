using System.Windows;

namespace Panuon.WPF.Builder
{
    public interface IModule
    {
        /// <summary>
        /// Returns the core ui element of this module.
        /// </summary>
        FrameworkElement Visual { get; }
                                                    
        /// <summary>
        /// Returns the ui element actually added to the view tree.
        /// If <see cref="Visual"/> has a parent element, the top-most parent element will be returned. Otherwise, <see cref="Visual"/> will be returned.
        /// </summary>
        FrameworkElement ActualVisual { get; }

        void Initialize();

        void SetValue(string propertyKeyOrName,
            object value,
            bool updateActualVisual = false);

        void SetValue(DependencyProperty property,
            object value,
            bool updateActualVisual = false);

        object GetValue(string propertyName);

        object GetValue(DependencyProperty property);
    }
}
