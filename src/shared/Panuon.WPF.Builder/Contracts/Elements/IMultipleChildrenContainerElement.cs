using System;
using System.Collections.Generic;
using System.Text;

namespace Panuon.WPF.Builder
{
    public interface IPanelElement
        : IContainerElement
    {
        IEnumerable<IModule> Children { get; }
    }
}
