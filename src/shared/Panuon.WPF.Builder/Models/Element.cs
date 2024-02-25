using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
namespace Panuon.WPF.Builder
{
    public abstract class Element
        : Module, IElement
    {
        #region Fields
        private readonly Dictionary<Delegate, Delegate> _createdHandlers =
            new Dictionary<Delegate, Delegate>();
        private IDictionary<string, object> _config;
        #endregion

        #region Ctor
        public Element(IDictionary<string, object> config)
        {
            _config = config;
        }
        #endregion

        #region Properties
        public abstract Type VisualType { get; }
        #endregion

        #region Protected Methods
        public void AddEventHandler(string eventName,
            Delegate handler)
        {
            var eventInfo = VisualType.GetEvent(eventName);
            handler = CreateEventHandler(eventInfo.EventHandlerType, handler);
            eventInfo.AddEventHandler(Visual, handler);
        }

        public void RemoveEventHandler(string eventName,
            Delegate handler)
        {
            var eventInfo = VisualType.GetEvent(eventName);

            if (_createdHandlers.ContainsKey(handler))
            {
                handler = _createdHandlers[handler];
                _createdHandlers.Remove(handler);
            }
            eventInfo.RemoveEventHandler(Visual, handler);
        }

        public void AddRoutedEventHandler(RoutedEvent @event,
            Delegate handler)
        {
            handler = CreateEventHandler(@event.HandlerType, handler);
            Visual.AddHandler(@event, handler);
        }

        public void RemoveRoutedEventHandler(RoutedEvent @event,
            Delegate handler)
        {
            if (_createdHandlers.ContainsKey(handler))
            {
                handler = _createdHandlers[handler];
                _createdHandlers.Remove(handler);
            }
            Visual.RemoveHandler(@event, handler);
        }

        protected override bool SetPropertyValue(string propertyKey,
            object value)
        {
            switch(propertyKey)
            {
                case "style":
                    SetValue(FrameworkElement.StyleProperty, value, true);
                    return true;
                case "visibility":
                    SetValue(FrameworkElement.VisibilityProperty, value, true);
                    return true;
                case "width":
                    SetValue(FrameworkElement.WidthProperty, value, true);
                    return true;
                case "height":
                    SetValue(FrameworkElement.HeightProperty, value, true);
                    return true;
                case "minwidth":
                    SetValue(FrameworkElement.MinWidthProperty, value, true);
                    return true;
                case "minheight":
                    SetValue(FrameworkElement.MinHeightProperty, value, true);
                    return true;
                case "maxwidth":
                    SetValue(FrameworkElement.MaxWidthProperty, value, true);
                    return true;
                case "maxheight":
                    SetValue(FrameworkElement.MaxHeightProperty, value, true);
                    return true;
                case "vertical":
                    SetValue(FrameworkElement.VerticalAlignmentProperty, value, true);
                    return true;
                case "horizontal":
                    SetValue(FrameworkElement.HorizontalAlignmentProperty, value, true);
                    return true;
                case "margin":
                    SetValue(FrameworkElement.MarginProperty, value, true);
                    return true;
                case "padding":
                    SetValue(Control.PaddingProperty, value);
                    return true;
                case "background":
                    SetValue(Control.BackgroundProperty, value);
                    return true;
                case "foreground":
                    SetValue(Control.ForegroundProperty, value);
                    return true;
                case "borderbrush":
                    SetValue(Control.BorderBrushProperty, value);
                    return true;
                case "borderthickness":
                    SetValue(Control.BorderThicknessProperty, value);
                    return true;
                case "fontsize":
                    SetValue(Control.FontSizeProperty, value);
                    return true;
                case "fontfamily":
                    SetValue(Control.FontFamilyProperty, value);
                    return true;
                case "fontstyle":
                    SetValue(Control.FontStyleProperty, value);
                    return true;
                case "fontweight":
                    SetValue(Control.FontWeightProperty, value);
                    return true;
                case "fontstretch":
                    SetValue(Control.FontStretchProperty, value);
                    return true;
            }

            return base.SetPropertyValue(propertyKey, value);
        }

        protected override FrameworkElement OnCreatingVisual()
        {
            return (FrameworkElement)Activator.CreateInstance(VisualType);
        }

        protected override void OnInitializing()
        {
            base.OnInitializing();

            foreach (var propertyValue in _config)
            {
                if (propertyValue.Value != null)
                {
                    SetPropertyValue(propertyValue.Key.ToLower(), propertyValue.Value);
                }
            }
        }
        #endregion

        #region Functions
        private Delegate CreateEventHandler(Type eventHandlerType,
            Delegate handler)
        {
            var handlerType = handler.GetType();

            if (eventHandlerType != handlerType)
            {
                try
                {
                    var newHandler = Delegate.CreateDelegate(eventHandlerType, handler.Target, handler.Method);
                    _createdHandlers.Add(handler, newHandler);
                    return newHandler;
                }
                catch
                {

                }
            }

            return handler;
        }
        #endregion
    }
}
