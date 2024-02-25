namespace Panuon.WPF.Builder
{
    public interface ITextBlockElement
        : IElement
    {
        ITextBlockElement CustomStyle(object foreground = null);

        ITextBlockElement AddRun(object text = null,
            object foreground = null);
    }
}
