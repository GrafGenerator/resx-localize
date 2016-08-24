using System.Linq;

namespace GrafGenerator.ResxLocalize.XlsxToJsonCommand
{
    public static class StringArrayExtensions
    {
        public static bool HasValues(this string[] array)
        {
            return array.Any(s => !string.IsNullOrEmpty(s));
        }
    }
}