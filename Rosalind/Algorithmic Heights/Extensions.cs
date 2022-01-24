public static class Extensions
{
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

    public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> sequence, Func<T, bool> splitFunction)
    {
        var list = new List<List<T>>();
        foreach (var elem in sequence)
        {
            if (splitFunction(elem))
            {
                list.Add(new List<T> { elem });
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
}