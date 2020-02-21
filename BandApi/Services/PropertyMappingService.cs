using BandApi.Entities;
using BandApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BandApi.Services
{
    public class PropertyMappingService : IPropertyMappingMarker, IPropertyMappingService
    {
        private Dictionary<string, PropertyMappingValue> _bandPropertyMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
        {
            { nameof(BandDto.Id), new PropertyMappingValue(new List<string>() { nameof(Band.Id)}) },
            { nameof(BandDto.Name), new PropertyMappingValue(new List<string>() { nameof(Band.Name)}) },
            { nameof(BandDto.MainGenre), new PropertyMappingValue(new List<string>() { nameof(Band.MainGenre)}) },
            { nameof(BandDto.FoundedYearsAgo), new PropertyMappingValue(new List<string>() { nameof(Band.Founded)}, revert: true) },
        };

        private IList<IPropertyMappingMarker> _propertyMappings = new List<IPropertyMappingMarker>();

        public PropertyMappingService()
        {
            _propertyMappings.Add(new PropertyMapping<BandDto, Band>(_bandPropertyMapping));
        }

        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
        {
            var matchingMapping = _propertyMappings.OfType<PropertyMapping<TSource, TDestination>>();

            return matchingMapping.FirstOrDefault()?.MappingDictionary ?? throw new Exception("No mapping was found");
        }

        public bool ValidMappingExists<TSource, TDestination>(string fields)
        {
            var propertyMapping = GetPropertyMapping<TSource, TDestination>();

            if (string.IsNullOrWhiteSpace(fields))
                return true;

            var fieldsAfterSplit = fields.Split(",");
            foreach(string field in fieldsAfterSplit)
            {
                string trimmerField = field.Trim();

                int indexOfSpace = trimmerField.IndexOf(" ");
                string propName = indexOfSpace == -1 ? trimmerField : trimmerField.Remove(indexOfSpace);

                if (!propertyMapping.ContainsKey(propName))
                    return false;
            }

            return true;
        }
    }
}
