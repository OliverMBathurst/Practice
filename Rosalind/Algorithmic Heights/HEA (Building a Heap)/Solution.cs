static void BubbleUp<T>(T[] arr, int index) where T : IComparable<T>
{
    int leftIdx = (index * 2) + 1, rightIdx = leftIdx + 1, largestIdx = index;
    if (leftIdx < arr.Length && arr[leftIdx].CompareTo(arr[largestIdx]) > 0)
        largestIdx = leftIdx;

    if (rightIdx < arr.Length && arr[rightIdx].CompareTo(arr[largestIdx]) > 0)
        largestIdx = rightIdx;

    if (largestIdx.CompareTo(index) != 0)
    {
        var tmp = arr[largestIdx];
        arr[largestIdx] = arr[index];
        arr[index] = tmp;

        BubbleUp(arr, largestIdx);
    }
}

static void BuildMaxHeap<T>(T[] arr) where T : IComparable<T>
{
    for (var i = (arr.Length / 2) - 1; i >= 0; i--)
        BubbleUp(arr, i);
}

var arr = File.ReadAllLines(@"C:\Users\Oliver\Desktop\rosalind_hea.txt")
    .Skip(1)
    .SelectMany(x => x.Split(' ').Select(int.Parse))
    .ToArray();

BuildMaxHeap(arr);

foreach (var number in arr)
    Console.Write($"{number} ");