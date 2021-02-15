using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;

namespace Common
{
    public static class Extentions
    {
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

        /* Generics */
        public static object GetProperty(this object obj, string propertyName, object[] parameters = null)
        {
            return obj
                .GetType()
                .GetProperty(propertyName)
                .GetMethod
                .Invoke(obj, parameters);
        }

        public static object ChangeType(this object value, Type conversion)
        {
            var t = conversion;

            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                t = Nullable.GetUnderlyingType(t);
            }

            return Convert.ChangeType(value, t);
        }

        /* Linq Expressions */
        public static Expression<Func<T, object>> GetKeySelected<T>(this string orderByPropName) where T : class
        {
            var param = Expression.Parameter(typeof(T));

            var prop = Expression.Property(param, orderByPropName ?? Constants.DbConstants.Id);

            return Expression.Lambda<Func<T, object>>(Expression.Convert(prop, typeof(object)), param);
        }

        public static Expression<Func<T, bool>> GetLessThanExpression<T>(this string orderByPropName, object value) where T : class
        {
            var parameter = Expression.Parameter(typeof(T));
            var left = Expression.Property(parameter, orderByPropName ?? Constants.DbConstants.Id);
            var castedValue = Expression.Convert(Expression.Constant(value.ChangeType(left.Type)), left.Type);
            var lessThanExpr = Expression.LessThanOrEqual(left, castedValue);
            return Expression.Lambda<Func<T, bool>>(lessThanExpr, new[] { parameter });
        }

        public static Expression<Func<T, bool>> GetGreaterThanExpression<T>(this string orderByPropName, object value) where T : class
        {
            var parameter = Expression.Parameter(typeof(T));
            var left = Expression.Property(parameter, orderByPropName ?? Constants.DbConstants.Id);
            var castedValue = Expression.Convert(Expression.Constant(value.ChangeType(left.Type)), left.Type);
            var lessThanExpr = Expression.GreaterThanOrEqual(left, castedValue);
            return Expression.Lambda<Func<T, bool>>(lessThanExpr, new[] { parameter });
        }

        public static Expression<Func<T, bool>> GetEqualExpression<T>(this string orderByPropName, object value) where T : class
        {
            var parameter = Expression.Parameter(typeof(T));
            var left = Expression.Property(parameter, orderByPropName ?? Constants.DbConstants.Id);
            var castedValue = Expression.Convert(Expression.Constant(value.ChangeType(left.Type)), left.Type);
            var lessThanExpr = Expression.Equal(left, castedValue);
            return Expression.Lambda<Func<T, bool>>(lessThanExpr, new[] { parameter });
        }
    }
}
