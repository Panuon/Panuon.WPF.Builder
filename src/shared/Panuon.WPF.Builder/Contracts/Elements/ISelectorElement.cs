using System;
using System.Windows.Controls;

namespace Panuon.WPF.Builder
{
    public interface ISelectorElement
        : IItemsControlElement
    {
        object SelectedItem { get; set; }

        int SelectedIndex { get; set; }


        ISelectorElement OnSelectionChanged(SelectionChangedEventHandler handler);
    }
}
