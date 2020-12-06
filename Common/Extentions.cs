using System;
using System.Globalization;

namespace Common
{
    public static class Extentions
    {
        public static object GetProperty(this object obj, string propertyName, object[] parameters = null)
        {
            return obj
                .GetType()
                .GetProperty(propertyName)
                .GetMethod
                .Invoke(obj, parameters);
        }

        public static DateTimeOffset? ParseIntoDateTime(this string datetime)
        {
            if (string.IsNullOrWhiteSpace(datetime))
                return null;
            return DateTimeOffset.ParseExact(
                datetime, 
                new string[] {"yyyy:MM:dd HH:mm:ss"},
                CultureInfo.InvariantCulture.DateTimeFormat,
                DateTimeStyles.AllowWhiteSpaces);
        }

        public static string RemoveUntilSpace(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return null;
            return text.Substring(0, text.IndexOf(" "));
        }
    }
}
