using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CSVInventoryStorage.Utils
{
    public static class Reflection
    {
        /// <summary>
        /// Gets the base directory of the executing assembly.
        /// </summary>
        /// <returns>Base directory of the executing assembly.</returns>
        public static string BaseDirectory() => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        /// <summary>
        /// Gets all keys of an enum as a list.
        /// </summary>
        /// <returns>Keys of the enum as a list.</returns>
        /// <typeparam name="T">Type of the enum.</typeparam>
        public static List<string> GetEnumKeys<T>() => Enum.GetNames(typeof(T)).ToList();

        /// <summary>
        /// Converts an enum to a dictionary.
        /// </summary>
        /// <returns>The enum as a dictionary.</returns>
        /// <param name="caseSensitive">If set to <c>true</c>, the dictionary will have case sensitive keys.</param>
        /// <typeparam name="T">Type of the enum.</typeparam>
        public static Dictionary<string, int> EnumToDictionary<T>(bool caseSensitive = false)
        {
            var type = typeof(T);
            var final = new Dictionary<string, int>(caseSensitive ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase);
            var values = Enum.GetValues(type);
            foreach (var value in values)
                final.Add(Enum.GetName(type, value) ?? throw new InvalidOperationException(), (int)value);
            return final;
        }

        /// <summary>
        /// Gets all properties of a type.
        /// </summary>
        /// <returns>Properties of the type.</returns>
        /// <typeparam name="T">Type to get the properties of.</typeparam>
        public static List<string> GetProperties<T>()
        {
            return typeof(T).GetProperties()
                            .ToList()
                            .Select(prop => prop.Name)
                            .ToList();
        }

        /// <summary>
        /// Gets all properties of an object's type.
        /// </summary>
        /// <returns>Properties of the object's type.</returns>
        /// <typeparam name="T">Object the properties of.</typeparam>
        public static List<string> GetProperties(object obj)
        {
            return obj.GetType()
                      .GetProperties()
                      .ToList()
                      .Select(prop => prop.Name)
                      .ToList();
        }

        /// <summary>
        /// Gets an object's value by it's property name.
        /// </summary>
        /// <returns>The value of the property.</returns>
        /// <param name="propertyName">Property to get the value of.</param>
        /// <param name="obj">Object to get the property from.</param>
        public static object GetValue(string propertyName, object obj)
        {
            var property = obj.GetType()
                              .GetProperties()
                              .ToList()
                              .Find(prop => prop.Name == propertyName);

            return property.GetValue(obj, null);
        }

        /// <summary>
        /// Gets an object's value by it's property name and casts it to <c>T</c>.
        /// </summary>
        /// <returns>The value of the property casted to <c>T</c>.</returns>
        /// <param name="propertyName">Property to get the value of.</param>
        /// <param name="obj">Object to get the property from.</param>
        public static T GetValue<T>(string propertyName, object obj)
        {
            var property = obj.GetType()
                              .GetProperties()
                              .ToList()
                              .Find(prop => prop.Name == propertyName);

            return property == null ? default(T) : (T)property.GetValue(obj, null);
        }

        /// <summary>
        /// Gets an object's properties and their values as a dictionary.
        /// </summary>
        /// <returns>The dictionary.</returns>
        /// <param name="obj">Object to get properties and values of.</param>
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
