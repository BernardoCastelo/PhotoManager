using System;
using System.Globalization;
using System.Linq.Expressions;
using static Common.Constants;

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

        public static Expression<Func<T, bool>> GetExpression<T>(this string orderByPropName, object value, WhereConditions type) where T : class
        {
            var parameter = Expression.Parameter(typeof(T));
            var left = Expression.Property(parameter, orderByPropName ?? DbConstants.Id);
            var castedValue = Expression.Convert(Expression.Constant(value?.ChangeType(left.Type)), left.Type);

            Expression exp = null;

            switch (type)
            {
                case WhereConditions.LessOrEqualThan:
                    exp = Expression.LessThanOrEqual(left, castedValue);
                    break;
                case WhereConditions.GreaterOrEqualThan:
                    exp = Expression.GreaterThanOrEqual(left, castedValue);
                    break;
                case WhereConditions.Equal:
                    exp = Expression.Equal(left, castedValue);
                    break;
                case WhereConditions.NotEqual:
                    exp = Expression.NotEqual(left, castedValue);
                    break;
                default:
                    break;
            }             
            return Expression.Lambda<Func<T, bool>>(exp, new[] { parameter });
        }        

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left, Expression<Func<T, bool>> right) where T : class
        {
            var parameter = Expression.Parameter(typeof(T));
            var exp = Expression.Or(left, right);
            return Expression.Lambda<Func<T, bool>>(exp, new[] { parameter });
        }
    }
}
