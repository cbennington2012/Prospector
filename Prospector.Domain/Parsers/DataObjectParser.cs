using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Prospector.Domain.Attributes;
using Prospector.Domain.Contracts.Parsers;
using Prospector.Domain.Contracts.Wrappers;

namespace Prospector.Domain.Parsers
{
    public class DataObjectParser : IDataObjectParser
    {
        private readonly IStructureMapWrapper _structureMapWrapper;

        public DataObjectParser(IStructureMapWrapper structureMapWrapper)
        {
            _structureMapWrapper = structureMapWrapper;
        }

        public T GetObject<T>(DataRow item)
        {
            var properties = typeof(T).GetProperties();
            var result = _structureMapWrapper.GetInstance(typeof(T));
            foreach (
                var property in
                    properties.Where(
                        property =>
                            typeof(T).GetProperty(property.Name).GetCustomAttribute(typeof(IgnoreAttribute), false) ==
                            null))
            {
                property.SetValue(result, property.PropertyType.BaseType == typeof(Enum)
                    ? EnumParser.GetEnumFromValue(item[property.Name].ToString(), property.PropertyType)
                    : CleanInput(item[property.Name], property.PropertyType));
            }

            return (T)result;
        }

        internal T CleanInput<T>(T input, Type type)
        {
            var illegalCharacters = new List<string> { "\0", Environment.NewLine };

            if (input is DBNull)
            {
                return default(T);
            }

            if (type == typeof(string))
            {
                var cleanedString = (input as string);
                cleanedString = illegalCharacters.Aggregate(cleanedString,
                    (current, item) => current.Replace(item, string.Empty));

                return (T)Convert.ChangeType(cleanedString, typeof(T));
            }
            return input;
        }
    }
}