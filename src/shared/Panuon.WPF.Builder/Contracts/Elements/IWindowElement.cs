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

        void SetOwner(object owner);

        IWindowElement OnClosing(CancelEventHandler handler);
    }
}
