using Panuon.WPF;
using Panuon.WPF.Builder;
using System.Windows;

namespace Samples
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            IoC.Set<IAppBuilder>(new AppBuilder());
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            AppBuilder.AddThemeResourceDictionary("light",
               "pack://application:,,,/Samples;component/Themes/Light.xaml");
            AppBuilder.AddThemeResourceDictionary("dark",
               "pack://application:,,,/Samples;component/Themes/Dark.xaml");

            AppBuilder.CurrentTheme = "light";

            var builder = IoC.Get<IAppBuilder>();
            builder.Show<HomeView>();
        }
    }
}
