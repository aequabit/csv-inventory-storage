using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CSVInventoryStorage.Utils
{
    public static class Crypto
    {
        public static string SHA256(string value)
        {
            using (SHA256 hash = new SHA256Managed())
            {
                return string.Concat(hash
                  .ComputeHash(Encoding.UTF8.GetBytes(value))
                  .Select(item => item.ToString("x2")));
            }
        }
    }
}