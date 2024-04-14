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
        private Type _windowType;
        #endregion

        #region Ctor
        internal WindowElement(Type windowType,
            IDictionary<string, object> config)
            : base(config)
        {
            _windowType = windowType ?? typeof(Window);
        }
        #endregion

        #region Properties
        public override Type VisualType => _windowType;
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

        public void SetOwner(object owner)
        {
            if (owner is Window ownerWindow)
            {
                WindowVisual.Owner = ownerWindow;
            }
            else if (owner is FrameworkElement ownerElement)
            {
                WindowVisual.Owner = Window.GetWindow(ownerElement);
            }
            else if (owner is IModule ownerModule)
            {
                if (ownerModule.ActualVisual is Window ownerWindowModule)
                {
                    WindowVisual.Owner = ownerWindowModule;
                }
                else if (ownerModule.ActualVisual is FrameworkElement ownerElementModule)
                {
                    WindowVisual.Owner = Window.GetWindow(ownerElementModule);
                }
            }
        }
        #endregion

        #region Overrides
        protected override bool SetPropertyValue(string propertyKey, object value)
        {
            switch (propertyKey)
            {
                case "owner":
                    SetOwner(value);
                    return true;
                case "title":
                    SetValue(Window.TitleProperty, value);
                    return true;
                case "location":
                    WindowVisual.WindowStartupLocation = SerializeValue<WindowStartupLocation>(value);
                    return true;
            }

            return base.SetPropertyValue(propertyKey, value);
        }
        #endregion
    }
}
