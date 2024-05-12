using System.ComponentModel;

namespace Panuon.WPF.Builder
{
    public interface IWindowElement
        : IContentControlElement
    {
        void Show();

        bool? ShowDialog();

        void Hide();

        void Close();

        IWindowElement OnClosing(CancelEventHandler handler);
    }
}
