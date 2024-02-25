using System.Collections.Generic;

namespace Panuon.WPF.Builder
{
    public interface IPanelElement
        : IElement
    {
        IEnumerable<IModule> Children { get; }

        void AddChild(IModule module);

        void InsertChild(IModule module, int index);

        void RemoveChild(IModule module);

        void RemoveChildAt(int index);
    }
}
