using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CSVInventoryStorage.Serialization
{
    class CsvSerializer
    {
        static readonly Dictionary<Type, Func<object, string>> _serializers = new Dictionary<Type, Func<object, string>>
        {
            { typeof(DateTime), (object val) => ((DateTime)val).ToString("dd.MM.yyyy")}
        };

        static readonly Dictionary<Type, Func<string, object>> _deserializers = new Dictionary<Type, Func<string, object>>
        {
            { typeof(DateTime), (string val) => {
                  DateTime parsed;
                  var      success = DateTime.TryParse(val, out parsed);
                  if (!success) return null;

                  return parsed;
              } }
        };

        // TODO: cleanup
        static string TrimQuotes(string str)
        {
            if (str.StartsWith("\"", StringComparison.OrdinalIgnoreCase))
                str = str.Substring(0, str.IndexOf("\"", StringComparison.OrdinalIgnoreCase));

            if (str.EndsWith("\"", StringComparison.OrdinalIgnoreCase))
                str = str.Substring(0, str.LastIndexOf("\"", StringComparison.OrdinalIgnoreCase));

            return str;
        }

        /// <summary>
        /// Escapes a value for use in CSV.
        /// </summary>
        /// <param name="val">Value to escape.</param>

        static string Escape(string val)
        {
            if (val.Contains("\""))
                val = val.Replace("\"", "\"\"");

            if (val.Contains(";") || val.Contains("\r") || val.Contains("\n"))
                val = "\"" + val + "\""; // TODO: cleanup

            return val;
        }

        /// <summary>
        /// Gets the CSV headers from an object.
        /// </summary>
        /// <param name="obj">Object to get the fields from.</param>
        public static string Headers(object obj)
        {
            var props = obj.GetType().GetProperties().Where(
                p => p.GetCustomAttributes(typeof(CsvSerializableAttribute), true).Length != 0);

            var header = props.Aggregate("", (current, prop) => current + (prop.Name + ";"));

            if (header.EndsWith(";", StringComparison.OrdinalIgnoreCase))
                header = header.Substring(0, header.LastIndexOf(";", StringComparison.Ordinal));

            return header;
        }

        /// <summary>
        /// Gets the CSV headers from a type.
        /// </summary>
        /// <param name="type">Type to get the fields from.</param>
        public static string Headers(Type type)
        {
            var props = type.GetProperties().Where(
                p => p.GetCustomAttributes(typeof(CsvSerializableAttribute), true).Length != 0);

            var header = props.Aggregate("", (current, prop) => current + (prop.Name + ";"));

            if (header.EndsWith(";", StringComparison.OrdinalIgnoreCase))
                header = header.Substring(0, header.LastIndexOf(";", StringComparison.Ordinal));

            return header;
        }

        /// <summary>
        /// Serializes an object.
        /// </summary>
        /// <param name="obj">Object to serialize.</param>
        public static string Serialize(object obj)
        {
            var props = obj.GetType().GetProperties().Where(
                p => p.GetCustomAttributes(typeof(CsvSerializableAttribute), true).Length != 0);

            var values = new List<string>();

            for (var i = 0; i < props.Count(); i++)
            {
                var prop = props.ElementAt(i);
                var type = prop.PropertyType;
                var val = prop.GetValue(obj, null);

                values.Add(_serializers.ContainsKey(type) ? _serializers[type](val) : Escape(val.ToString()));

                if (i < props.Count() - 1)
                    values.Add(";");
            }

            return string.Concat(values);
        }

        /// <summary>
        /// Deserializes CSV data to an object.
        /// </summary>
        /// <param name="csv">CSV to deserialize.</param>
        public static T Deserialize<T>(string csv)
        {
            var props = Headers(typeof(T)).Split(';').ToList();
            var values = csv.Split(';').ToList();

            var obj = (T)Activator.CreateInstance(typeof(T));

            for (var i = 0; i < props.Count(); i++)
            {
                var propName = props.ElementAt(i);
                var prop = obj.GetType().GetProperty(propName, BindingFlags.Public | BindingFlags.Instance);

                if (prop == null || !prop.CanWrite) continue;

                var type = prop.PropertyType;

                var val = values.ElementAt(i);

                object final;
                if (_deserializers.ContainsKey(type))
                    final = _deserializers[type](val);
                else
                    final = val.Trim('"');

                prop.SetValue(obj, final, null);
            }

            return obj;
        }
    }
}
