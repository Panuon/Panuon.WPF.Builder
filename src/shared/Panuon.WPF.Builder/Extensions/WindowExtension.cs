using Panuon.WPF.Builder.Elements;
using Panuon.WPF.Builder.Utils;
using System;
using System.Reflection;

namespace Panuon.WPF.Builder
{
    public static class WindowExtension
    {
        public static IWindowElement CreateWindow(this IAppBuilder appBuilder,
            object content= null,
            object owner = null,
            Type type = null,
            object title = null,
            object icon = null,
            object style = null,
            object width = null,
            object height = null,
            object location = null)
        {
            var config = ParameterUtil.GetParameters(MethodBase.GetCurrentMethod(),
                appBuilder,
                content,
                owner, type,
                title, icon, 
                style,
                width, height,
                location
                );

            return new WindowElement(type, config);
        }
    }
}
