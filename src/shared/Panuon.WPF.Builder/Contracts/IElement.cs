using System;
using System.Windows;

namespace Panuon.WPF.Builder
{
    public interface IElement
        : IModule
    {
        Type VisualType { get; }

        void AddEventHandler(string eventName,
          Delegate handler);

        void RemoveEventHandler(string eventName,
            Delegate handler);

        void AddRoutedEventHandler(RoutedEvent @event,
            Delegate handler);

        void RemoveRoutedEventHandler(RoutedEvent @event,
            Delegate handler);
    }
}
