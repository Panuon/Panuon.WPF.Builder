namespace Panuon.WPF.Builder
{
    public interface ITabControlElement
        : ISelectorElement
    {
        ITabControlElement AddItem(object header,
            object content);
    }
}
