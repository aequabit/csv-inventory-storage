using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CSVInventoryStorage.Serialization
{
    public class BinarySerializer
    {
        public static byte[] Serialize(object obj)
        {
            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream()) {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

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
