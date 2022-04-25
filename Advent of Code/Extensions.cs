public static class Extensions
{
    public static bool ContainsKeys<K, V>(this IDictionary<K, V> dictionary, params K[] keys)
    {
        return keys.All(k => dictionary.ContainsKey(k));
    }

    public static T FirstOrElse<T>(this IEnumerable<T> collection, Func<T, bool> predicate, T @default)
    {
        var matchingElements = collection.Where(predicate);
        return matchingElements.Any() ? matchingElements.First() : @default;
    }

    public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action)
    {
        foreach (var obj in sequence)
        {
            action(obj);
        }
    }

    public static void Repeat(this int times, Action action)
    {
        for (var i = 0; i < times; i++)
        {
            action();
        }
    }

    public static IEnumerable<IEnumerable<T>> Split<T>(
        this IEnumerable<T> sequence,
        Func<T, bool> splitFunction,
        bool include = true)
    {
        var list = new List<List<T>>();
        foreach (var elem in sequence)
        {
            if (splitFunction(elem))
            {
                var @new = new List<T>();
                if (include)
                    @new.Add(elem);
                list.Add(@new);
            }
            else
            {
                if (list.Count == 0)
                    list.Add(new List<T>());
                list[^1].Add(elem);
            }
        }

        return list;
    }

    public static IDictionary<K, List<K>> ToUndirectedVUDictionary<K>(
        this IEnumerable<KeyValuePair<K, K>> keyValuePairs) where K : notnull
    {
        var dict = new Dictionary<K, List<K>>();
        foreach (var kvp in keyValuePairs)
        {
            if (!dict.ContainsKey(kvp.Key))
                dict.Add(kvp.Key, new List<K> { kvp.Value });
            else
                dict[kvp.Key].Add(kvp.Value);

            if (!dict.ContainsKey(kvp.Value))
                dict.Add(kvp.Value, new List<K> { kvp.Key });
            else
                dict[kvp.Value].Add(kvp.Key);
        }

        return dict;
    }

    public static void AddOrUpdate<K, V>(
        this IDictionary<K, V> dictionary,
        KeyValuePair<K, V> addValue,
        Action<V> updateAction)
    {
        if (!dictionary.ContainsKey(addValue.Key))
            dictionary.Add(addValue);
        else
            updateAction(dictionary[addValue.Key]);
    }

    public static IDictionary<K, V> ToDictionary<K, V, T>(
        this IEnumerable<T> sequence,
        Func<T, K> keySelector,
        Func<T, int, V> addFunction,
        Action<T, int, V> updateFunction) where K : notnull
    {
        var dict = new Dictionary<K, V>();
        var idx = 0;
        foreach (var elem in sequence)
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


    public static TAggregationResult? AggregateOrDefault<T, TAggregationResult, TAggregationOne, TAggregationTwo>(
        this IEnumerable<T> sequence,
        Func<TAggregationOne, TAggregationTwo, TAggregationResult> aggregationTransformer,
        Func<IEnumerable<T>, TAggregationOne> firstAggregationFunction,
        Func<IEnumerable<T>, TAggregationTwo> secondAggregationFunction)
    {
        if (sequence == null || !sequence.Any())
        {
            return default;
        }

        return aggregationTransformer(firstAggregationFunction(sequence), secondAggregationFunction(sequence));
    }
}