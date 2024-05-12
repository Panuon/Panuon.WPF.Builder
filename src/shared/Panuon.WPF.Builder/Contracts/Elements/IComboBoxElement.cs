using System;
using System.Windows.Controls;

namespace Panuon.WPF.Builder
{
    public interface IComboBoxElement
        : ISelectorElement
    {
        new IComboBoxElement OnSelectionChanged(SelectionChangedEventHandler handler);
    }
}
