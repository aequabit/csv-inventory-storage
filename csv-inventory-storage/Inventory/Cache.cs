using System;
using System.Collections.Generic;
using System.IO;
using CSVInventoryStorage.Serialization;
using CSVInventoryStorage.Utils;

namespace CSVInventoryStorage.Inventory
{
    public static class Cache
    {
        /// <summary>
        /// Base path of the storage directory.
        /// </summary>
        static string StorageDirectory = Reflection.BaseDirectory() + "/inventoryCache/";

        /// <summary>
        /// Initializes the cache.
        /// </summary>
		public static void Initialize()
        {
            if (!Directory.Exists(StorageDirectory))
                Directory.CreateDirectory(StorageDirectory);
        }

        /// <summary>
        /// Caches an item if it isn't in the cache yet.
        /// </summary>
        /// <returns><c>true</c>, if item was cached, <c>false</c> if it was already in the cache.</returns>
        /// <param name="item">Item.</param>
        public static bool CacheItem(Item item)
        {
            if (ItemCached(item))
                return false;

            var bytes = BinarySerializer.Serialize(item);
            File.WriteAllBytes(StorageDirectory + Crypto.SHA256(bytes), bytes);

            return true;
        }

        /// <summary>
        /// Checks if an item is in the cache.
        /// </summary>
        /// <returns><c>true</c>, if the item is in the cache, <c>false</c> otherwise.</returns>
        /// <param name="item">Item.</param>
        public static bool ItemCached(Item item) => File.Exists(StorageDirectory + HashItem(item));

		/// <summary>
		/// Checks if an item's hash is in the cache.
		/// </summary>
		/// <returns><c>true</c>, if the hash is in the cache, <c>false</c> otherwise.</returns>
		/// <param name="hash">Hash of the item.</param>
		public static bool ItemCached(string hash) => File.Exists(StorageDirectory + hash);

        /// <summary>
        /// Retrieves an item by it's hash.
        /// </summary>
        /// <returns>The item.</returns>
        /// <param name="hash">Hash of the item to retrieve.</param>
        public static Item RetrieveItem(string hash)
        {
            if (!ItemCached(hash))
                return null;

            var bytes = File.ReadAllBytes(StorageDirectory + hash);

            try
            {
                return BinarySerializer.Deserialize<Item>(bytes);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Retrieves all cached items.
        /// </summary>
        /// <returns>Retrieved items.</returns>
        public static List<Item> RetrieveItems()
        {
            var final = new List<Item>();

            foreach (var file in Directory.EnumerateFiles(StorageDirectory))
                final.Add(RetrieveItem(Path.GetFileName(file)));

            final.RemoveAll(item => item == null);

            return final;
        }

        /// <summary>
        /// Overwrites the cache with a list of items.
        /// </summary>
        /// <param name="items">Items.</param>
        public static void SetItems(List<Item> items)
        {
            ClearCache();

            foreach (var item in items)
                CacheItem(item);
        }

        /// <summary>
        /// Clears the cache.
        /// </summary>
        public static void ClearCache()
        {
            foreach (var file in Directory.EnumerateFiles(StorageDirectory))
                File.Delete(file);
        }

        /// <summary>
        /// Removes an item from the cache.
        /// </summary>
        /// <returns><c>true</c>, if item was removed, <c>false</c> otherwise.</returns>
        /// <param name="item">Item to add.</param>
        public static bool RemoveItem(Item item)
        {
            if (!ItemCached(item))
                return false;

            File.Delete(StorageDirectory + HashItem(item));
            return true;
        }

        /// <summary>
        /// Hashes an item
        /// </summary>
        /// <returns>Hash of the item.</returns>
        /// <param name="item">Item to hash.</param>
        static string HashItem(Item item)
        {
            var bytes = BinarySerializer.Serialize(item);
            return Crypto.SHA256(bytes);
        }
    }
}
