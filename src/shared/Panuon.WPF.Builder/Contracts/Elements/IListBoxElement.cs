using System;
using System.Windows.Controls;

namespace Panuon.WPF.Builder
{
    public interface IListBoxElement
        : ISelectorElement
    {
        new IListBoxElement OnSelectionChanged(SelectionChangedEventHandler handler);
    }
}
