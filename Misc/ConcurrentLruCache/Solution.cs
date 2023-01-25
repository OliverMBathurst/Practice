using System.Collections.Concurrent;

public class ConcurrentLruCache<K, V> where K : notnull, IComparable<K>
{
    private readonly ConcurrentDictionary<K, long> _useCache = new();
    private readonly object _useCacheLock = new(), _cacheLock = new();
    private readonly ConcurrentDictionary<K, V> _cache;
    private readonly int _capacity;

    public ConcurrentLruCache(int capacity)
    {
        if (capacity <= 0)
        {
            throw new ArgumentException("Capacity must be greater than 0");
        }

        _capacity = capacity;
        _cache = new ConcurrentDictionary<K, V>(Environment.ProcessorCount * 2, capacity);
    }

    public ConcurrentLruCache(int concurrencyLevel, int capacity)
    {
        if (capacity <= 0)
        {
            throw new ArgumentException("Capacity must be greater than 0");
        }

        _capacity = capacity;
        _cache = new ConcurrentDictionary<K, V>(concurrencyLevel, capacity);
    }

    public V GetOrDefault(K key)
    {
        lock (_cacheLock)
        {
            if (_cache.ContainsKey(key))
            {
                _useCache.AddOrUpdate(key, 1, (key, value) => value + 1);
                return _cache[key];
            }
        }

        return default;
    }

    public void Put(K key, V value)
    {
        lock (_cacheLock)
        {
            if (_cache.ContainsKey(key))
            {
                _cache[key] = value;
                _useCache.AddOrUpdate(key, 1, (key, value) => value + 1);
                return;
            }

            if (_cache.Count == _capacity)
            {
                lock (_useCacheLock)
                {
                    var minUsages = _useCache.MinBy(x => x.Value);
                    _useCache.TryRemove(minUsages);

                    var minCache = _cache.FirstOrDefault(kvp => kvp.Key.CompareTo(minUsages.Key) == 0);
                    _cache.TryRemove(minCache);
                }
            }

            _useCache.AddOrUpdate(key, 1, (key, val) => _cache.TryAdd(key, value) ? 1 : val + 1);
        }
    }
}