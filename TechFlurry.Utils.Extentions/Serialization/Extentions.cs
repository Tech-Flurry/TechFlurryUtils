using Newtonsoft.Json;
using System;

namespace TechFlurry.Utils.Extentions.Serialization
{
    public static class Extentions
    {
        public static string ToJson(this object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return json;
        }
        public static object ToObject(this string json, Type type)
        {
            var obj = JsonConvert.DeserializeObject(json, type);
            return obj;
        }
        public static T ToObject<T>(this string json)
        {
            var obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }
    }
}
