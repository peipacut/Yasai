using System.Collections.Generic;
using System.Diagnostics;

namespace Yasai.Structures
{
    /// <summary>
    /// Injects dependencies
    /// </summary>
    public class DependencyCache 
    {
        private Dictionary<string, object> cache;

        public DependencyCache() => cache = new Dictionary<string, object>();

        private string getContext<T>(string c="default") => $"{typeof(T)}_{c}";

        /// <summary>
        /// Store something in the cache with an autogenerated context.
        /// The context being of the form "[Type name]_default"
        /// </summary>
        /// <param name="item"></param>
        /// <typeparam name="T"></typeparam>
        public void Store<T>(T item) 
            => Store(item, getContext<T>());

        /// <summary>
        /// Store something in the cache with a context
        /// </summary>
        /// <param name="item"></param>
        /// <param name="context"></param>
        /// <typeparam name="T"></typeparam>
        public void Store<T>(T item, string context) 
            => cache[getContext<T>(context)] = item;

        /// <summary>
        /// Retrieve a dependency from the cache
        /// </summary>
        /// <param name="key"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Retrieve<T>(string key = null)
        {
            var k = getContext<T>(key ?? getContext<T>());
            if (!cache.ContainsKey(k))
                throw new KeyNotFoundException($"no such {key} of type {typeof(T)} in the internal cache");
            
            return (T)cache[k];
        }
    }
}