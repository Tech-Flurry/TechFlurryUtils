using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TechFlurry.Utils.Functions.Common
{
    public static class Functions
    {
        public static Timer RunOnThread(Action action)
        {
            return new Timer(new TimerCallback(_ =>
            {
                action.Invoke();
            }), null, TimeSpan.FromMilliseconds(100), Timeout.InfiniteTimeSpan);
        }
        public static Timer SetTimeout(Action action, TimeSpan delay)
        {
            return new Timer(new TimerCallback(_ =>
            {
                action.Invoke();
            }), null, delay, Timeout.InfiniteTimeSpan);
        }
        public static Timer SetInterval(Action action, TimeSpan interval)
        {
            return new Timer(new TimerCallback(_ =>
            {
                action.Invoke();
            }), null, TimeSpan.FromMilliseconds(100), interval);
        }
        public static void SetInterval(Action action, TimeSpan interval, TimeSpan delay)
        {
            var timer = new Timer(new TimerCallback(_ =>
            {
                action.Invoke();
            }), null, delay, interval);
        }
        public static DateTime GetDefaultDate()
        {
            return new DateTime(2021, 1, 1);
        }
        public static string ValueToId(long value)
        {
            var parts = new List<string>();
            long numberPart = value % 100000;
            parts.Add(numberPart.ToString("00000"));
            value /= 100000;
            var alphaParts = new List<string>();
            for (int i = 0; i < 3 || value > 0; ++i)
            {
                alphaParts.Add(((char)(65 + (value % 26))).ToString());
                value /= 26;
            }
            parts.Add(string.Join("", alphaParts));
            return string.Join("-", parts.AsEnumerable().Reverse().ToArray());
        }
        public static string ValueToShortId(int value)
        {
            var parts = new List<string>();
            long numberPart = value % 100;
            parts.Add(numberPart.ToString("000"));
            value /= 100;
            var alphaParts = new List<string>();
            for (int i = 0; i < 1 || value > 0; ++i)
            {
                alphaParts.Add(((char)(65 + (value % 26))).ToString());
                value /= 26;
            }
            parts.Add(string.Join("", alphaParts));
            return string.Join("", parts.AsEnumerable().Reverse().ToArray());
        }
        public static string GenerateElementId()
        {
            return "t" + Guid.NewGuid().ToString().Replace("-", string.Empty);
        }
        public static string ConvertToOracleDate(DateTime date)
        {
            return $"TO_DATE('{date:dd/MM/yyyy HH:mm:ss}', 'DD/MM/YYYY HH24:MI:SS')";
        }
        public static string GetFileExtensionFromBLOB(string base64String)
        {
            var data = base64String.Substring(0, 5);

            switch (data.ToUpper())
            {
                case "IVBOR":
                    return "png";
                case "/9J/4":
                    return "jpg";
                case "AAAAF":
                    return "mp4";
                case "JVBER":
                    return "pdf";
                case "AAABA":
                    return "ico";
                case "UMFYI":
                    return "rar";
                case "E1XYD":
                    return "rtf";
                case "U1PKC":
                    return "txt";
                case "MQOWM":
                case "77U/M":
                    return "srt";
                default:
                    return string.Empty;
            }
        }
        public static T MapOnConventions<T>(Dictionary<string, object> keyValuePairs, Func<string, string, bool> convention)
        {
            var obj = Activator.CreateInstance<T>();
            var objType = typeof(T);
            foreach (var kvp in keyValuePairs)
            {
                foreach (var prop in objType.GetProperties())
                {
                    if (convention.Invoke(prop.Name, kvp.Key))
                    {
                        try
                        {
                            prop.SetValue(obj, kvp.Value);
                        }
                        catch (ArgumentException)
                        {
                            if (!prop.PropertyType.IsNullableEnum())
                            {
                                var changedType = Convert.ChangeType(kvp.Value, prop.PropertyType);
                                prop.SetValue(obj, changedType);
                            }
                            else
                            {
                                var enumType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                                var changedType = Enum.ToObject(enumType, kvp.Value);
                                prop.SetValue(obj, changedType, null);
                            }
                        }
                    }
                }
            }
            return obj;
        }
        private static bool IsNullableEnum(this Type t)
        {
            Type u = Nullable.GetUnderlyingType(t);
            return (u != null) && u.IsEnum;
        }
    }
}
