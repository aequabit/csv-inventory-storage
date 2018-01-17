using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CSVInventoryStorage.Serialization
{
    public static class BinarySerializer
    {
        /// <summary>
        /// Serializes ab object to bytes.
        /// </summary>
        /// <returns>The serialized object.</returns>
        /// <param name="obj">Object to serialize.</param>
        public static byte[] Serialize(object obj)
        {
            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream()) {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Deserializes bytes to an object.
        /// </summary>
        /// <returns>The deserialized object.</returns>
        /// <param name="data">Bytes to deserialize.</param>
        /// <typeparam name="T">Type to deserialize to.</typeparam>
        public static T Deserialize<T>(byte[] data) {
            using(var ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                ms.Write(data, 0, data.Length);
                ms.Seek(0, SeekOrigin.Begin);
                var obj = bf.Deserialize(ms);
                return (T)obj;
            }
        }
    }
}
