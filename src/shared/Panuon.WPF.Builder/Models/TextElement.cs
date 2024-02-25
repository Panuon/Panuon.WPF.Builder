using System.Windows;
using System.Windows.Controls;

namespace Panuon.WPF.Builder
{
    public abstract class DocumentTextElement
        : Module, IElement
    {
        #region Fields
        private readonly Dictionary<Delegate, Delegate> _createdHandlers =
            new Dictionary<Delegate, Delegate>();
        private IDictionary<string, object> _config;
        #endregion

        #region Ctor
        public TextElement(IDictionary<string, object> config)
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

        protected object GetConfig(string configKey)
        {
            if (_config.ContainsKey(configKey))
            {
                return _config[configKey];
            }
            return null;
        }

        protected T GetConfig<T>(string configKey)
        {
            return SerializeValue<T>(GetConfig(configKey));
        }

        protected override void OnInitializing()
        {
            if (GetConfig("style") is object style)
            {
                SetValue(TextElement.StyleProperty,
                    style, true);
            }
            if (GetConfig("width") is object width)
            {
                SetValue(FrameworkTextElement.WidthProperty,
                    width, true);
            }
            if (GetConfig("minWidth") is object minWidth)
            {
                SetValue(FrameworkTextElement.MinWidthProperty,
                    minWidth, true);
            }
            if (GetConfig("maxWidth") is object maxWidth)
            {
                SetValue(FrameworkTextElement.MaxWidthProperty,
                    maxWidth, true);
            }
            if (GetConfig("height") is object height)
            {
                SetValue(FrameworkTextElement.HeightProperty,
                    height, true);
            }
            if (GetConfig("minHeight") is object minHeight)
            {
                SetValue(FrameworkTextElement.MinHeightProperty,
                    minHeight, true);
            }
            if (GetConfig("maxHeight") is object maxHeight)
            {
                SetValue(FrameworkTextElement.MaxHeightProperty,
                    maxHeight, true);
            }
            if (GetConfig("vertical") is object vertical)
            {
                SetValue(FrameworkTextElement.VerticalAlignmentProperty,
                    vertical, true);
            }
            if (GetConfig("horizontal") is object horizontal)
            {
                SetValue(FrameworkTextElement.HorizontalAlignmentProperty,
                    horizontal, true);
            }
            if (GetConfig("margin") is object margin)
            {
                SetValue(FrameworkTextElement.MarginProperty,
                    margin, true);
            }
            if (ActualVisual is Control)
            {
                if (GetConfig("padding") is object padding)
                {
                    SetValue(Control.PaddingProperty,
                        padding);
                }
                if (GetConfig("background") is object background)
                {
                    SetValue(Control.BackgroundProperty,
                        background);
                }
                if (GetConfig("foreground") is object foreground)
                {
                    SetValue(Control.ForegroundProperty,
                        foreground);
                }
                if (GetConfig("borderBrush") is object borderBrush)
                {
                    SetValue(Control.BorderBrushProperty,
                        borderBrush);
                }
                if (GetConfig("borderThickness") is object borderThickness)
                {
                    SetValue(Control.BorderThicknessProperty,
                        borderThickness);
                }
            }
        }

        protected override FrameworkTextElement OnCreatingVisual()
        {
            return (FrameworkTextElement)Activator.CreateInstance(VisualType);
        }
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
