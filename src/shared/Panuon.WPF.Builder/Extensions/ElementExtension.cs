using System;
using System.Windows;
using System.Windows.Input;

namespace Panuon.WPF.Builder
{
    public static class ElementExtension
    {
        public static TElement AddHandle<TElement>(this TElement element,
            string eventName,
            Delegate handler)
            where TElement : IElement
        {
            element.AddEventHandler(eventName, handler);
            return element;
        }

        public static TElement AddHandle<TElement>(this TElement element,
            RoutedEvent @event,
            Delegate handler)
            where TElement : IElement
        {
            element.AddRoutedEventHandler(@event, handler);
            return element;
        }

        public static TElement OnPreviewDragEnter<TElement>(this TElement element,
           DragEventHandler handler)
           where TElement : IElement
        {
            element.AddRoutedEventHandler(FrameworkElement.PreviewDragEnterEvent, handler);
            return element;
        }

        public static TElement OnPreviewDragOver<TElement>(this TElement element,
           DragEventHandler handler)
           where TElement : IElement
        {
            element.AddRoutedEventHandler(FrameworkElement.PreviewDragOverEvent, handler);
            return element;
        }

        public static TElement OnPreviewDragLeave<TElement>(this TElement element,
           DragEventHandler handler)
           where TElement : IElement
        {
            element.AddRoutedEventHandler(FrameworkElement.PreviewDragLeaveEvent, handler);
            return element;
        }

        public static TElement OnPreviewMouseWheel<TElement>(this TElement element,
            MouseWheelEventHandler handler)
            where TElement : IElement
        {
            element.AddRoutedEventHandler(FrameworkElement.PreviewMouseWheelEvent, handler);
            return element;
        }

        public static TElement OnPreviewMouseUp<TElement>(this TElement element,
            MouseButtonEventHandler handler)
            where TElement : IElement
        {
            element.AddRoutedEventHandler(FrameworkElement.PreviewMouseUpEvent, handler);
            return element;
        }

        public static TElement OnPreviewMouseLeftButtonUp<TElement>(this TElement element,
            MouseButtonEventHandler handler)
            where TElement : IElement
        {
            element.AddRoutedEventHandler(FrameworkElement.PreviewMouseLeftButtonUpEvent, handler);
            return element;
        }

        public static TElement OnPreviewMouseRightButtonUp<TElement>(this TElement element,
            MouseButtonEventHandler handler)
            where TElement : IElement
        {
            element.AddRoutedEventHandler(FrameworkElement.PreviewMouseRightButtonUpEvent, handler);
            return element;
        }

        public static TElement OnPreviewMouseDown<TElement>(this TElement element,
            MouseButtonEventHandler handler)
            where TElement : IElement
        {
            element.AddRoutedEventHandler(FrameworkElement.PreviewMouseUpEvent, handler);
            return element;
        }
        
        public static TElement OnPreviewMouseLeftButtonDown<TElement>(this TElement element,
            MouseButtonEventHandler handler)
            where TElement : IElement
        {
            element.AddRoutedEventHandler(FrameworkElement.PreviewMouseLeftButtonDownEvent, handler);
            return element;
        }

        public static TElement OnPreviewMouseRightButtonDown<TElement>(this TElement element,
            MouseButtonEventHandler handler)
            where TElement : IElement
        {
            element.AddRoutedEventHandler(FrameworkElement.PreviewMouseRightButtonDownEvent, handler);
            return element;
        }

        public static TElement OnPreviewKeyUp<TElement>(this TElement element,
            KeyEventHandler handler)
            where TElement : IElement
        {
            element.AddRoutedEventHandler(FrameworkElement.PreviewKeyUpEvent, handler);
            return element;
        }

        public static TElement OnPreviewKeyDown<TElement>(this TElement element,
            KeyEventHandler handler)
            where TElement : IElement
        {
            element.AddRoutedEventHandler(FrameworkElement.PreviewKeyDownEvent, handler);
            return element;
        }

        public static TElement OnPreviewGotKeyboardFocus<TElement>(this TElement element,
            KeyboardFocusChangedEventHandler handler)
            where TElement : IElement
        {
            element.AddRoutedEventHandler(FrameworkElement.PreviewGotKeyboardFocusEvent, handler);
            return element;
        }

        public static TElement OnPreviewLostKeyboardFocus<TElement>(this TElement element,
            KeyboardFocusChangedEventHandler handler)
            where TElement : IElement
        {
            element.AddRoutedEventHandler(FrameworkElement.PreviewLostKeyboardFocusEvent, handler);
            return element;
        }

        public static TElement OnPreviewTextInput<TElement>(this TElement element,
            TextCompositionEventHandler handler)
            where TElement : IElement
        {
            element.AddRoutedEventHandler(FrameworkElement.PreviewTextInputEvent, handler);
            return element;
        }

        public static TElement OnDragEnter<TElement>(this TElement element,
          DragEventHandler handler)
          where TElement : IElement
        {
            element.AddRoutedEventHandler(FrameworkElement.DragEnterEvent, handler);
            return element;
        }

        public static TElement OnDragOver<TElement>(this TElement element,
           DragEventHandler handler)
           where TElement : IElement
        {
            element.AddRoutedEventHandler(FrameworkElement.DragOverEvent, handler);
            return element;
        }

        public static TElement OnDragLeave<TElement>(this TElement element,
           DragEventHandler handler)
           where TElement : IElement
        {
            element.AddRoutedEventHandler(FrameworkElement.DragLeaveEvent, handler);
            return element;
        }

        public static TElement OnTouchLeave<TElement>(this TElement element,
            EventHandler<TouchEventArgs> handler)
            where TElement : IElement
        {
            element.AddRoutedEventHandler(FrameworkElement.TouchLeaveEvent, handler);
            return element;
        }

        public static TElement OnTouchEnter<TElement>(this TElement element,
            EventHandler<TouchEventArgs> handler)
            where TElement : IElement
        {
            element.AddRoutedEventHandler(FrameworkElement.TouchEnterEvent, handler);
            return element;
        }

        public static TElement OnTouchDown<TElement>(this TElement element,
            EventHandler<TouchEventArgs> handler)
            where TElement : IElement
        {
            element.AddRoutedEventHandler(FrameworkElement.TouchDownEvent, handler);
            return element;
        }

        public static TElement OnMouseWheel<TElement>(this TElement element,
            MouseWheelEventHandler handler)
            where TElement : IElement
        {
            element.AddRoutedEventHandler(FrameworkElement.MouseWheelEvent, handler);
            return element;
        }

        public static TElement OnMouseUp<TElement>(this TElement element,
            MouseButtonEventHandler handler)
            where TElement : IElement
        {
            element.AddRoutedEventHandler(FrameworkElement.MouseUpEvent, handler);
            return element;
        }

        public static TElement OnMouseLeftButtonUp<TElement>(this TElement element,
            MouseButtonEventHandler handler)
            where TElement : IElement
        {
            element.AddRoutedEventHandler(FrameworkElement.MouseLeftButtonUpEvent, handler);
            return element;
        }

        public static TElement OnMouseRightButtonUp<TElement>(this TElement element,
            MouseButtonEventHandler handler)
            where TElement : IElement
        {
            element.AddRoutedEventHandler(FrameworkElement.MouseRightButtonUpEvent, handler);
            return element;
        }

        public static TElement OnMouseDown<TElement>(this TElement element,
            MouseButtonEventHandler handler)
            where TElement : IElement
        {
            element.AddRoutedEventHandler(FrameworkElement.MouseUpEvent, handler);
            return element;
        }

        public static TElement OnMouseLeftButtonDown<TElement>(this TElement element,
            MouseButtonEventHandler handler)
            where TElement : IElement
        {
            element.AddRoutedEventHandler(FrameworkElement.MouseLeftButtonDownEvent, handler);
            return element;
        }

        public static TElement OnMouseRightButtonDown<TElement>(this TElement element,
            MouseButtonEventHandler handler)
            where TElement : IElement
        {
            element.AddRoutedEventHandler(FrameworkElement.MouseRightButtonDownEvent, handler);
            return element;
        }

        public static TElement OnKeyUp<TElement>(this TElement element,
            KeyEventHandler handler)
            where TElement : IElement
        {
            element.AddRoutedEventHandler(FrameworkElement.KeyUpEvent, handler);
            return element;
        }

        public static TElement OnKeyDown<TElement>(this TElement element,
            KeyEventHandler handler)
            where TElement : IElement
        {
            element.AddRoutedEventHandler(FrameworkElement.KeyDownEvent, handler);
            return element;
        }

        public static TElement OnGotKeyboardFocus<TElement>(this TElement element,
            KeyboardFocusChangedEventHandler handler)
            where TElement : IElement
        {
            element.AddRoutedEventHandler(FrameworkElement.GotKeyboardFocusEvent, handler);
            return element;
        }

        public static TElement OnLostKeyboardFocus<TElement>(this TElement element,
            KeyboardFocusChangedEventHandler handler)
            where TElement : IElement
        {
            element.AddRoutedEventHandler(FrameworkElement.LostKeyboardFocusEvent, handler);
            return element;
        }

        public static TElement OnTextInput<TElement>(this TElement element,
            TextCompositionEventHandler handler)
            where TElement : IElement
        {
            element.AddRoutedEventHandler(FrameworkElement.TextInputEvent, handler);
            return element;
        }
    }
}
