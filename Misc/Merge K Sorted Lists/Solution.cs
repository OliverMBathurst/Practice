var arrays = new int[][]
{
    new [] { 1, 4, 5 },
    new [] { 1, 3, 4 },
    new [] { 2, 6 }
};

foreach (var number in MergeKSortedLists(arrays))
    Console.Write($"{number}, ");

static IEnumerable<T> MergeKSortedLists<T>(T[][] lists) where T : IComparable<T>
{
    int resultingArraySize = 0, resultIndex = 0;
    for (var i = 0; i < lists.Length; i++)
        resultingArraySize += lists[i].Length;

    var indices = new int[lists.Length];
    while (resultIndex < resultingArraySize)
    {
        var hasSetMin = false;
        T? min = default;
        var minArrayIndex = 0;

        for (var i = 0; i < lists.Length; i++)
        {
            if (indices[i] < lists[i].Length && (!hasSetMin || lists[i][indices[i]].CompareTo(min) < 0))
            {
                min = lists[i][indices[i]];
                minArrayIndex = i;
                hasSetMin = true;
            }
        }

        if (!hasSetMin)
            yield break;

        yield return min;

        indices[minArrayIndex]++;

        resultIndex++;
    }
}