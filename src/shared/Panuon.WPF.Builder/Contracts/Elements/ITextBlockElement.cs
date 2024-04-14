namespace Panuon.WPF.Builder
{
    public interface ITextBlockElement
        : IElement
    {
        string Text { get; set; }

        ITextBlockElement CustomStyle(object foreground = null);

        ITextBlockElement AddRun(object text = null,
            object foreground = null);
    }
}
