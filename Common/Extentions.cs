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
            DateTimeOffset.TryParseExact(
                datetime,
                new string[] { "yyyy:MM:dd HH:mm:ss" },
                CultureInfo.InvariantCulture.DateTimeFormat,
                DateTimeStyles.AllowWhiteSpaces,
                out DateTimeOffset date);
            return date;
        }

        public static string RemoveUntilSpace(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return null;
            var index = text.IndexOf(" ");
            return index > 0 ? text.Substring(0, text.IndexOf(" ")) : text;
        }
    }
}
