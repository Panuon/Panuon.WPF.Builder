using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Panuon.WPF.Builder.Elements
{
    internal class WindowElement
        : ContentControlElement, IWindowElement
    {
        #region Fields
        #endregion

        #region Ctor
        internal WindowElement(IAppBuilder appBuilder, IDictionary<string, object> config)
            : base(appBuilder, config)
        {
        }
        #endregion

        #region Properties
        public override Type VisualType => typeof(Window);
        #endregion

        #region Internal Properties
        internal Window WindowVisual => Visual as Window;
        #endregion

        #region Methods
        public void Close()
        {
            WindowVisual.Close();
        }

        public void Hide()
        {
            WindowVisual.Hide();
        }

        public void Show()
        {
            WindowVisual.Show();
        }

        public bool? ShowDialog()
        {
            return WindowVisual.ShowDialog();
        }

        public IWindowElement OnClosing(CancelEventHandler handler)
        {
            AddEventHandler(nameof(WindowVisual.Closing), handler);
            return this;
        }
        #endregion

        #region Overrides
        public override void SetValue(string propertyNameOrKey, object value)
        {
            switch (propertyNameOrKey)
            {
                case "owner":
                    if (value is Window ownerWindow)
                    {
                        WindowVisual.Owner = ownerWindow;
                    }
                    else if (value is FrameworkElement ownerElement)
                    {
                        WindowVisual.Owner = Window.GetWindow(ownerElement);
                    }
                    else if (value is IModule ownerModule)
                    {
                        if (ownerModule.Visual is Window ownerWindowModule)
                        {
                            WindowVisual.Owner = ownerWindowModule;
                        }
                        else if (ownerModule.Visual is FrameworkElement ownerElementModule)
                        {
                            WindowVisual.Owner = Window.GetWindow(ownerElementModule);
                        }
                    }
                    break;
                case "location":
                    WindowVisual.WindowStartupLocation = SerializeValue<WindowStartupLocation>(value);
                    break;
                case "title":
                    SetValue(Window.TitleProperty, value);
                    break;
                default:
                    base.SetValue(propertyNameOrKey, value);
                    break;
            }
        }
        #endregion
    }
}