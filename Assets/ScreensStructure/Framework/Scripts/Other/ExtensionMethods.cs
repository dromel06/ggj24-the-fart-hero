using System;
using System.Text.RegularExpressions;

namespace GodFramework
{
    public static class ExtensionMethods
    {
        public static string SplitCamelCase(this string value)
        {
            return Regex.Replace(value, "(?<=[a-z])([A-Z])", " $1");
        }

        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}