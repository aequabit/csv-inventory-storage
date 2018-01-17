using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CSVInventoryStorage.Utils
{
    public static class Crypto
    {
        /// <summary>
        /// Calculates the SHA-256 hash of a string.
        /// </summary>
        /// <returns>SHA-256 hash of <c>value</c>.</returns>
        /// <param name="value">String to hash.</param>
        public static string SHA256(string value) => SHA256(Encoding.UTF8.GetBytes(value));

		/// <summary>
		/// Calculates the SHA-256 hash of bytes.
		/// </summary>
		/// <returns>SHA-256 hash of <c>value</c>.</returns>
		/// <param name="value">Bytes to hash.</param>
		public static string SHA256(byte[] value)
		{
			using (SHA256 hash = new SHA256Managed())
			{
				return string.Concat(hash
				  .ComputeHash(value)
				  .Select(item => item.ToString("x2")));
			}
		}
    }
}