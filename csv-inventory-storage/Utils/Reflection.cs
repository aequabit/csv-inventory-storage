using System;
using System.Collections.Generic;
using System.Linq;

namespace CSVInventoryStorage.Utils
{
    public static class Reflection
    {
		public static List<string> GetEnumKeys<T>() => Enum.GetNames(typeof(T)).ToList();

		public static Dictionary<string, int> EnumToDictionary<T>(bool caseSensitive = false)
        {
            var type = typeof(T);
            var final = new Dictionary<string, int>(caseSensitive ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase);
            var values = Enum.GetValues(type);
            foreach (var value in values)
		        final.Add(Enum.GetName(type, value) ?? throw new InvalidOperationException(), (int)value);
	        return final;
        }

        public static List<string> GetProperties<T>()
        {
            return typeof(T).GetProperties()
                            .ToList()
                            .Select(prop => prop.Name)
                            .ToList();
        }

        public static List<string> GetProperties(object obj)
        {
            return obj.GetType()
                      .GetProperties()
                      .ToList()
                      .Select(prop => prop.Name)
                      .ToList();
        }

        public static object GetValue(string propertyName, object obj)
        {
            var property = obj.GetType()
                              .GetProperties()
                              .ToList()
                              .Find(prop => prop.Name == propertyName);

            return property.GetValue(obj, null);
        }

        public static T GetValue<T>(string propertyName, object obj)
        {
            var property = obj.GetType()
                              .GetProperties()
                              .ToList()
                              .Find(prop => prop.Name == propertyName);

            return property == null ? default(T) : (T)property.GetValue(obj, null);
        }

        public static Dictionary<string, object> Get(object obj)
        {
            var properties = obj.GetType()
                              .GetProperties()
                              .ToList();

            var final = new Dictionary<string, object>();
            properties.ForEach(prop => final.Add(prop.Name, prop.GetValue(obj, null)));
            return final;
        }
    }
}
