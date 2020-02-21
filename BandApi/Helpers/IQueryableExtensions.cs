using BandApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace BandApi.Helpers
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string orderBy,
            Dictionary<string, PropertyMappingValue> mappingDictionary)
        {
            if (source == null)
                throw new ArgumentException(nameof(source));
            if (mappingDictionary == null)
                throw new ArgumentException(nameof(mappingDictionary));

            if (string.IsNullOrWhiteSpace(orderBy))
                return source;

            var orderBySplit = orderBy.Split(",");

            var orderByString = "";
            foreach (var orderByClause in orderBySplit)
            {
                var trimmerOrderBy = orderByClause.Trim();
                var orderDesc = trimmerOrderBy.EndsWith(" DESC", StringComparison.InvariantCultureIgnoreCase);
                var indexOfSpace = trimmerOrderBy.IndexOf(" ");
                var propertyName = indexOfSpace == -1 ? trimmerOrderBy : trimmerOrderBy.Remove(indexOfSpace);

                if (!mappingDictionary.ContainsKey(propertyName))
                    throw new ArgumentException("Mapping does not exists for " + propertyName);

                var propertyMappingValue = mappingDictionary[propertyName];

                if (propertyMappingValue == null)
                    throw new ArgumentNullException(nameof(propertyName));

                foreach(var destination in propertyMappingValue.DestinationProperties.Reverse())
                {
                    if (propertyMappingValue.Revert)
                        orderDesc = !orderDesc;

                    
                    orderByString += (!string.IsNullOrWhiteSpace(orderByString) ? "," : "") +
                        destination + (orderDesc ? " descending" : " ascending");
                }
            }

            return source.OrderBy(orderByString);
        }
    }
}
