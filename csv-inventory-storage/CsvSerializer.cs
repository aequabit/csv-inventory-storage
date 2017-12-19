using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CSVInventoryStorage
{
    class CsvSerializer
    {
        public static Dictionary<Type, Func<object, string>> _serializers = new Dictionary<Type, Func<object, string>>()
        {
            { typeof(DateTime), (object val) => ((DateTime)val).ToString("dd.MM.yyyy")},
        };

        public static Dictionary<Type, Func<string, object>> _deserializers = new Dictionary<Type, Func<string, object>>()
        {
            { typeof(DateTime), (string val) => {
                  DateTime parsed;
                  var      success = DateTime.TryParse(val, out parsed);
                  if (!success) return null;

                  return parsed;
              } }
        };

        // Source: https://stackoverflow.com/a/6377656
        /// <summary>
        /// Escapes a value for use in CSV.
        /// </summary>
        /// <param name="val">Value to escape.</param>
        public static string Escape(string val)
        {
            if (val.Contains(";"))
                throw new System.Exception("Semicolon not allowed in CSV value");

            bool mustQuote = (val.Contains(",") || val.Contains("\"") || val.Contains("\r") || val.Contains("\n"));

            if (!mustQuote)
                return val;

            var sb = new StringBuilder();
            sb.Append("\"");
            foreach (var nextChar in val)
            {
                if (nextChar == '"')
                    sb.Append("”");
                else
                    sb.Append(nextChar);
            }
            sb.Append("\"");
            return sb.ToString();
        }

        /*/// <summary>
        /// Escapes a value for use in CSV.
        /// </summary>
        /// <param name="val">Value to escape.</param>

        private string Escape(string val)
        {
         var final = val;
        
         if (final.Contains('"'))
             final.Replace("\"", "”");
        
         if (final.Contains(";"))
             final = "\"" + final + "\"";         // TODO: cleanup
        
         return final;
        }*/

        /// <summary>
        /// Gets the CSV headers from an object.
        /// </summary>
        /// <param name="obj">Object to get the fields from.</param>
        public static string Headers(object obj)
        {
            var props = obj.GetType().GetProperties().Where(
                p => p.GetCustomAttributes(typeof(CsvSerializableAttribute), true).Length != 0);

            var header = props.Aggregate("", (current, prop) => current + (prop.Name + ";"));

            if (header.EndsWith(";"))
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

            if (header.EndsWith(";"))
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

                values.Add(_serializers.ContainsKey(type) ? _serializers[type]((object)val) : Escape(val.ToString()));

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

        // TODO: cleanup
        private static string _trimQuotes(string str)
        {
            if (str.StartsWith("\""))
                str = str.Substring(0, str.IndexOf("\"", StringComparison.Ordinal));

            if (str.EndsWith("\""))
                str = str.Substring(0, str.LastIndexOf("\"", StringComparison.Ordinal));

            return str;
        }
    }
}
