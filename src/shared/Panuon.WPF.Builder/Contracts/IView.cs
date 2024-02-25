namespace Panuon.WPF.Builder
{
    public interface IView
        : IModule
    {
        IElement RootElement { get; }

        void Close(bool? dialogResult);
    }
}
