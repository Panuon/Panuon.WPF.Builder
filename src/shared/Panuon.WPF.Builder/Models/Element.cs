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
        #endregion

        #region Ctor
        public Element(IAppBuilder appBuilder, 
            IDictionary<string, object> config)
            : base(appBuilder)
        {
            foreach (var propertyValue in config)
            {
                if (propertyValue.Value != null)
                {
                    SetValue(propertyValue.Key.ToLower(), propertyValue.Value);
                }
            }
        }
        #endregion

        #region Properties

        #endregion

        #region Methods

        #region AddEventHandler
        public void AddEventHandler(string eventName,
            Delegate handler)
        {
            var eventInfo = VisualType.GetEvent(eventName);
            handler = CreateEventHandler(eventInfo.EventHandlerType, handler);
            eventInfo.AddEventHandler(Visual, handler);
        }
        #endregion

        #region RemoveEventHandler
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
        #endregion

        #region AddRoutedEventHandler
        public void AddRoutedEventHandler(RoutedEvent @event,
            Delegate handler)
        {
            handler = CreateEventHandler(@event.HandlerType, handler);
            Visual.AddHandler(@event, handler);
        }
        #endregion

        #region RemoveRoutedEventHandler
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
        #endregion

        #region SetValue
        public override void SetValue(string propertyNameOrKey, object value)
        {
            switch (propertyNameOrKey)
            {
                case "style":
                    SetValue(FrameworkElement.StyleProperty, value);
                    break;
                case "visibility":
                    SetValue(FrameworkElement.VisibilityProperty, value);
                    break;
                case "isenabled":
                    SetValue(FrameworkElement.IsEnabledProperty, value);
                    break;
                case "width":
                    SetValue(FrameworkElement.WidthProperty, value);
                    break;
                case "height":
                    SetValue(FrameworkElement.HeightProperty, value);
                    break;
                case "minwidth":
                    SetValue(FrameworkElement.MinWidthProperty, value);
                    break;
                case "minheight":
                    SetValue(FrameworkElement.MinHeightProperty, value);
                    break;
                case "maxwidth":
                    SetValue(FrameworkElement.MaxWidthProperty, value);
                    break;
                case "maxheight":
                    SetValue(FrameworkElement.MaxHeightProperty, value);
                    break;
                case "vertical":
                    SetValue(FrameworkElement.VerticalAlignmentProperty, value);
                    break;
                case "horizontal":
                    SetValue(FrameworkElement.HorizontalAlignmentProperty, value);
                    break;
                case "margin":
                    SetValue(FrameworkElement.MarginProperty, value);
                    break;
                case "padding":
                    SetValue(Control.PaddingProperty, value);
                    break;
                case "background":
                    SetValue(Control.BackgroundProperty, value);
                    break;
                case "foreground":
                    SetValue(Control.ForegroundProperty, value);
                    break;
                case "borderbrush":
                    SetValue(Control.BorderBrushProperty, value);
                    break;
                case "borderthickness":
                    SetValue(Control.BorderThicknessProperty, value);
                    break;
                case "fontsize":
                    SetValue(Control.FontSizeProperty, value);
                    break;
                case "fontfamily":
                    SetValue(Control.FontFamilyProperty, value);
                    break;
                case "fontstyle":
                    SetValue(Control.FontStyleProperty, value);
                    break;
                case "fontweight":
                    SetValue(Control.FontWeightProperty, value);
                    break;
                case "fontstretch":
                    SetValue(Control.FontStretchProperty, value);
                    break;
                default:
                    base.SetValue(propertyNameOrKey, value);
                    break;
            }
        }
        #endregion

        #region OnInitializing
        protected override void OnInitializing()
        {
            base.OnInitializing();
        }
        #endregion

        #region OnCreatingVisual
        protected override FrameworkElement OnCreatingVisual()
        {
            return (FrameworkElement)Activator.CreateInstance(VisualType);
        }
        #endregion

        #endregion

        #region Functions
        private Delegate CreateEventHandler(Type eventHandlerType,
            Delegate handler)
        {
            var handlerType = handler.GetType();

            if (eventHandlerType != handlerType)
            {
                var newHandler = Delegate.CreateDelegate(eventHandlerType, handler.Target, handler.Method);
                _createdHandlers.Add(handler, newHandler);
                return newHandler;
            }

            return handler;
        }
        #endregion
    }
}
