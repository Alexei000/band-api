using System;
using System.Reflection;

namespace BandApi.Services
{
    public class PropertyValidationService : IPropertyValidationService
    {
        public bool HasValidProperties<T>(string fields)
        {
            if (string.IsNullOrWhiteSpace(fields))
                return true;

            var splitFields = fields.Split(",", StringSplitOptions.RemoveEmptyEntries);
            foreach (var field in splitFields)
            {
                string propertyName = field.Trim();
                var propertyInfo = typeof(T).GetProperty(propertyName,
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propertyInfo == null)
                    return false;
            }

            return true;
        }
    }
}
