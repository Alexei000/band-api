﻿using System;
using System.Collections.Generic;

namespace BandApi.Services
{
    public class PropertyMapping<TSource, TDestination> : IPropertyMappingMarker
    {
        public Dictionary<string, PropertyMappingValue> MappingDictionary { get; set; }

        public PropertyMapping(Dictionary<string, PropertyMappingValue> mappingDictionary)
        {
            MappingDictionary = mappingDictionary ?? throw new ArgumentNullException(nameof(mappingDictionary));
        }
    }
}
