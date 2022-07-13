using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace TechFlurry.Utils.MetronicComponents.Common
{
    internal static class Functions
    {
        public static string GenerateElementId()
        {
            return "e_" + Guid.NewGuid().ToString().Replace("-", string.Empty);
        }
        public static string GetPropertyName<T>(Expression<Func<T, object>> property)
        {
            if (property.Body is not MemberExpression memberExpression)
            {
                try
                {
                    memberExpression = ((UnaryExpression)property.Body).Operand as MemberExpression;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("Expression is not a MemeberExpression");
                }
            }

            PropertyInfo propertyInfo = (PropertyInfo)memberExpression.Member;
            Type propertyType = propertyInfo.PropertyType;
            string name = propertyInfo.Name;
            string text = memberExpression.ToString();
            string[] array = text.Split(new char[1] { '.' });
            if (array.Length > 2)
            {
                return string.Join(".", array.Skip(1));
            }

            return memberExpression.Member.Name;
        }

        public static Type GetPropertyType<T>(Expression<Func<T, object>> property)
        {
            MemberExpression memberExpression = property.Body as MemberExpression;
            if (memberExpression == null)
            {
                try
                {
                    memberExpression = ((UnaryExpression)property.Body).Operand as MemberExpression;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("Expression is not a MemeberExpression");
                }
            }

            PropertyInfo propertyInfo = (PropertyInfo)memberExpression.Member;
            return propertyInfo.PropertyType;
        }

        public static Tuple<T, T> GetMinMaxValue<T>()
        {
            object obj = default(T);
            object obj2 = default(T);
            if (obj == null || obj2 == null)
            {
                return null;
            }

            switch (Type.GetTypeCode(typeof(T)))
            {
                case TypeCode.Byte:
                    obj = (byte)0;
                    obj2 = byte.MaxValue;
                    break;
                case TypeCode.Char:
                    obj = '\0';
                    obj2 = '\uffff';
                    break;
                case TypeCode.DateTime:
                    obj = DateTime.MinValue;
                    obj2 = DateTime.MaxValue;
                    break;
                case TypeCode.Decimal:
                    obj = decimal.MinValue;
                    obj2 = decimal.MaxValue;
                    break;
                case TypeCode.Double:
                    obj = double.MinValue;
                    obj2 = double.MaxValue;
                    break;
                case TypeCode.Int16:
                    obj = short.MinValue;
                    obj2 = short.MaxValue;
                    break;
                case TypeCode.Int32:
                    obj = int.MinValue;
                    obj2 = int.MaxValue;
                    break;
                case TypeCode.Int64:
                    obj = long.MinValue;
                    obj2 = long.MaxValue;
                    break;
                case TypeCode.SByte:
                    obj = sbyte.MinValue;
                    obj2 = sbyte.MaxValue;
                    break;
                case TypeCode.Single:
                    obj = float.MinValue;
                    obj2 = float.MaxValue;
                    break;
                case TypeCode.UInt16:
                    obj = (ushort)0;
                    obj2 = ushort.MaxValue;
                    break;
                case TypeCode.UInt32:
                    obj = 0u;
                    obj2 = uint.MaxValue;
                    break;
                case TypeCode.UInt64:
                    obj = 0uL;
                    obj2 = ulong.MaxValue;
                    break;
            }

            return Tuple.Create((T)obj, (T)obj2);
        }

        public static bool IsNumber(Type type)
        {
            TypeCode typeCode = Type.GetTypeCode(type);
            TypeCode typeCode2 = typeCode;
            if ((uint)(typeCode2 - 5) <= 10u)
            {
                return true;
            }

            return false;
        }

        public static bool IsNumber(object obj)
        {
            return IsNumber(obj.GetType());
        }

        public static bool IsDate(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.DateTime:
                    return true;
                default:
                    return false;
            }
        }
        public static bool IsNumericOrDate(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                case TypeCode.DateTime:
                    return true;
                default:
                    return false;
            }
        }
    }
}
