namespace Panuon.WPF.Builder.Extensions
{
    public static class PanelExtension
    {
        public static TPanel AddChild<TPanel>(this TPanel panel,
            IModule child)
            where TPanel : IPanelElement
        {
            panel.AddChild(child);
            return panel;
        }

        public static TPanel InsertChild<TPanel>(this TPanel panel,
            IModule child,
            int index)
            where TPanel : IPanelElement
        {
            panel.InsertChild(child, index);
            return panel;
        }

        public static TPanel RemoveChild<TPanel>(this TPanel panel,
            IModule child)
            where TPanel : IPanelElement
        {
            panel.RemoveChild(child);
            return panel;
        }

        public static TPanel RemoveChild<TPanel>(this TPanel panel,
            int index)
            where TPanel : IPanelElement
        {
            panel.RemoveChild(index);
            return panel;
        }
    }
}
