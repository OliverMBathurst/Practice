public static class Extensions {
    public static IDictionary<K, V> ToDictionary<K, V, T>(
        this IEnumerable<T> collection,
        Func<T, K> keySelector,
        Func<T, int, V> addFunction,
        Action<T, int, V> updateFunction) where K : notnull
    {
        var dict = new Dictionary<K, V>();
        var idx = 0;
        foreach (var elem in collection)
        {
            var key = keySelector(elem);
            if (dict.ContainsKey(key))
                updateFunction(elem, idx, dict[key]);
            else
                dict.Add(key, addFunction(elem, idx));
            idx++;
        }
        return dict;
    }
}