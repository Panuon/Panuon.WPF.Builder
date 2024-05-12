using System;
using System.Windows;

namespace Panuon.WPF.Builder
{
    public interface IModule
    {
        /// <summary>
        /// Returns the core ui element of this module.
        /// </summary>
        FrameworkElement Visual { get; }
                                                    
        FrameworkElementFactory VisualFactory { get; }

        Type VisualType { get; }

        IAppBuilder AppBuilder { get; }

        void Initialize();

        void SetValue(string propertyKeyOrName,
            object value);

        void SetValue(DependencyProperty property,
            object value);

        object GetValue(string propertyName);

        object GetValue(DependencyProperty property);
    }
}
