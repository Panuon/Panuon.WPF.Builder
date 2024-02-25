using System;

namespace Panuon.WPF.Builder
{
    public interface IToggleButtonElement
        : IContentControlElement
    {
        IToggleButtonElement OnClick(Delegate handler);
    }
}
