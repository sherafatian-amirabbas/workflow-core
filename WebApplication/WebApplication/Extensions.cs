using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;


namespace WebApplication
{
    public static class Extensions
    {
        public static ExpandoObject JsonToExpando(this string json)
        {
            var expConverter = new ExpandoObjectConverter();
            return JsonConvert.DeserializeObject<ExpandoObject>(json, expConverter);
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> lst,
            Action<T> action)
        {
            foreach (T item in lst)
            {
                action(item);
            }

            return lst;
        }
    }
}
