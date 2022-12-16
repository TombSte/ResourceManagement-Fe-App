using System.Text.RegularExpressions;

namespace ResourceManagement_Fe_App.Components.Helpers
{
    public static class DoubleUtilities
    {
        public static string Parse(string value)
        {
            return Regex.Replace(value, @"\€\s?", "");
        }
        public static string Format(double value)
        {
            return "€ " + value.ToString();
        }
    }
}
