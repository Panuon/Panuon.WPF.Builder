using System.Windows;

namespace Panuon.WPF.Builder
{
    public interface IMenuElement
        : IItemsControlElement
    {
        IMenuElement OnItemClick(RoutedEventHandler handler);
    }
}
