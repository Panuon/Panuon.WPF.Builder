namespace Panuon.WPF.Builder
{
    public interface ITextBlockElement
        : IElement
    {
        string Text { get; set; }

        ITextBlockElement AddRun(object text = null,
            object toolTip = null,
            object foreground = null);
    }
}