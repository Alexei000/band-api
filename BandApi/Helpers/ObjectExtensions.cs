using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

namespace BandApi.Helpers
{
    public static class ObjectExtensions
    {
        public static ExpandoObject ShapeData<TSource>(this TSource source, string fields)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

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

            var dataShapedObject = new ExpandoObject();
            foreach (var propertyInfo in propertyInfoList)
            {
                var propValue = propertyInfo.GetValue(source);
                ((IDictionary<string, object>)dataShapedObject).Add(propertyInfo.Name, propValue);
            }

            return dataShapedObject;
        }
    }
}
