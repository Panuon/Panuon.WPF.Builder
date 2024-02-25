using System.Collections.Generic;
using System.Reflection;

namespace Panuon.WPF.Builder.Utils
{
    internal static class ParameterUtil
    {
        public static IDictionary<string, object> GetParameters(MethodBase method,
            params object[] parameters)
        {
            var dictionary = new Dictionary<string, object>();

            var paramInfos = method.GetParameters();

            for (int i = 1; i < paramInfos.Length; i++)
            {
                var paramInfo = paramInfos[i];
                dictionary.Add(paramInfos[i].Name, parameters[i]);
            }
            return dictionary;
        }
    }
}
