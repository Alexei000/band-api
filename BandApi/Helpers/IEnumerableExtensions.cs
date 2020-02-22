using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

namespace BandApi.Helpers
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<ExpandoObject> ShapeData<TSource>(this IEnumerable<TSource> source,
            string fields)
        {
            if (source == null)
                throw  new ArgumentNullException(nameof(source));

            var objectList = new List<ExpandoObject>();
            var propertyInfoList = new List<PropertyInfo>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                var propertyInfos = typeof(TSource).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                propertyInfoList.AddRange(propertyInfos);
            }
            else
            {
                string[] splitFields = fields.Split(",", StringSplitOptions.RemoveEmptyEntries);
                foreach (string field in splitFields)
                {
                    string propertyName = field.Trim();
                    var propertyInfo = typeof(TSource).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                    if (propertyInfo == null)
                        throw new Exception(propertyName + " was not found");

                    propertyInfoList.Add(propertyInfo);
                }
            }

            foreach (TSource obj in source)
            {
                var dataShapedObject = new ExpandoObject();
                foreach (var propertyInfo in propertyInfoList)
                {
                    var propValue = propertyInfo.GetValue(obj);
                    ((IDictionary<string, object>) dataShapedObject).Add(propertyInfo.Name, propValue);
                }

                objectList.Add(dataShapedObject);
            }

            return objectList;
        }
    }
}
