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
        public static string ToCSharpDateFormat(this DateFormats dateFormat)
        {
            var format = string.Empty;
            switch (dateFormat)
            {
                case DateFormats.MMddyyyy:
                    format = "MM/dd/yyyy";
                    break;
                case DateFormats.MMddyyyyHHmmss:
                    format = "MM/dd/yyyy HH:mm:ss";
                    break;
                case DateFormats.MMddyyyyHHmm:
                    format = "MM/dd/yyyy HH:mm";
                    break;
                case DateFormats.MMddyyyyHmm:
                    format = "MM/dd/yyyy H:mm";
                    break;
                case DateFormats.MMddyyyyhhmmtt:
                    format = "MM/dd/yyyy hh:mm tt";
                    break;
                case DateFormats.MMddyyyyhmmtt:
                    format = "MM/dd/yyyy h:mm tt";
                    break;
                case DateFormats.MMddyyHHmm:
                    format = "MM/dd/yy h:mm tt";
                    break;
                case DateFormats.ddMMyyyy:
                    format = "dd/MM/yyyy";
                    break;
                case DateFormats.ddMMyyyyHHmmss:
                    format = "dd/MM/yyyy HH:mm:ss";
                    break;
                case DateFormats.ddMMMMyyHHmm:
                    format = "dd MMMM, yy HH:mm";
                    break;
                case DateFormats.ddMMMMyyHmm:
                    format = "dd MMMM, yy H:mm";
                    break;
                case DateFormats.ddMMMMyyHHmmss:
                    format = "dd MMMM, yy HH:mm:ss";
                    break;
                case DateFormats.ddMMyyhhmmtt:
                    format = "dd/MM/yy hh:mm tt";
                    break;
                case DateFormats.ddMMyyhmmtt:
                    format = "dd/MM/yy h:mm tt";
                    break;
                case DateFormats.MMMyyyy:
                    format = "MMM, yyyy";
                    break;
                case DateFormats.MMMMyyyy:
                    format = "MMMM, yyyy";
                    break;
                case DateFormats.MMMMyy:
                    format = "MMMM, yy";
                    break;
                case DateFormats.yyyy:
                    format = "yyyy";
                    break;
                case DateFormats.yyyyMMdd:
                    format = "yyyy-MM-dd";
                    break;
                case DateFormats.yyyyMMddHHmmss:
                    format = "yyyy-MM-dd HH:mm:ss";
                    break;
            }
            return format;
        }
        public static string ToJSDateFormat(this DateFormats dateFormat)
        {
            var format = string.Empty;
            switch (dateFormat)
            {
                case DateFormats.MMddyyyy:
                    format = "MM/DD/YYYY";
                    break;
                case DateFormats.MMddyyyyHHmmss:
                    format = "MM/DD/YYYY HH:mm:ss";
                    break;
                case DateFormats.MMddyyyyHHmm:
                    format = "MM/DD/YYYY HH:mm";
                    break;
                case DateFormats.MMddyyyyHmm:
                    format = "MM/DD/YYYY H:mm";
                    break;
                case DateFormats.MMddyyyyhhmmtt:
                    format = "MM/DD/YYYY hh:mm tt";
                    break;
                case DateFormats.MMddyyyyhmmtt:
                    format = "MM/DD/YYYY h:mm tt";
                    break;
                case DateFormats.MMddyyHHmm:
                    format = "MM/DD/YYYY h:mm tt";
                    break;
                case DateFormats.ddMMyyyy:
                    format = "DD/MM/YYYY";
                    break;
                case DateFormats.ddMMyyyyHHmmss:
                    format = "DD/MM/YYYY HH:mm:ss";
                    break;
                case DateFormats.ddMMMMyyHHmm:
                    format = "DD MMMM, YY HH:mm";
                    break;
                case DateFormats.ddMMMMyyHmm:
                    format = "DD MMMM, YY H:mm";
                    break;
                case DateFormats.ddMMMMyyHHmmss:
                    format = "DD MMMM, YY HH:mm:ss";
                    break;
                case DateFormats.ddMMyyhhmmtt:
                    format = "DD/MM/YY hh:mm tt";
                    break;
                case DateFormats.ddMMyyhmmtt:
                    format = "DD/MM/YY h:mm tt";
                    break;
                case DateFormats.MMMyyyy:
                    format = "MMM, YYYY";
                    break;
                case DateFormats.MMMMyyyy:
                    format = "MMMM, YYYY";
                    break;
                case DateFormats.MMMMyy:
                    format = "MMMM, YY";
                    break;
                case DateFormats.yyyy:
                    format = "YYYY";
                    break;
                case DateFormats.yyyyMMdd:
                    format = "YYYY-MM-DD";
                    break;
                case DateFormats.yyyyMMddHHmmss:
                    format = "YYYY-MM-DD HH:mm:ss";
                    break;
            }
            return format;
        }
    }
}
