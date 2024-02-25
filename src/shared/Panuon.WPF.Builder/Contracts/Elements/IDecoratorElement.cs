namespace Panuon.WPF.Builder
{
    public interface IDecoratorElement
        : IContainerElement
    {
        IModule Child { get; set; }

        IGridElement GridSplit(IModule[] elements,
            string[] spans = null,
            object width = null,
            object height = null,
            object vertical = null,
            object horizontal = null,
            object background = null,
            object borderBrush = null,
            object borderThickness = null);

        void Stack(IModule[] elements,
            object orientation = null,
            object width = null,
            object height = null,
            object vertical = null,
            object horizontal = null,
            object background = null,
            object borderBrush = null,
            object borderThickness = null);
    }
}
