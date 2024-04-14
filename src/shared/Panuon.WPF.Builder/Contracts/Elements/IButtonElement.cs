using System.Windows;

namespace Panuon.WPF.Builder
{
    public interface IButtonElement
        : IContentControlElement
    {
        IButtonElement OnClick(RoutedEventHandler handler);
    }
}
