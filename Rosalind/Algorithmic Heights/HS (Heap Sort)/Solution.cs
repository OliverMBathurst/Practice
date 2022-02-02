static void MaxHeapify<T>(T[] arr, int n, int i) where T : IComparable<T>
{
    int max = i, left = (2 * i) + 1, right = left + 1;
    if (left < n && arr[left].CompareTo(arr[max]) > 0)
        max = left;

    if (right < n && arr[right].CompareTo(arr[max]) > 0)
        max = right;

    if (max != i)
    {
        var tmp = arr[max];
        arr[max] = arr[i];
        arr[i] = tmp;
        MaxHeapify(arr, n, max);
    }
}

static void BuildMaxHeap<T>(T[] arr) where T : IComparable<T>
{
    for (var i = (arr.Length / 2) - 1; i >= 0; i--)
        MaxHeapify(arr, arr.Length, i);
}

static void HeapSort<T>(T[] arr) where T : IComparable<T>
{
    BuildMaxHeap(arr);
    for (var i = arr.Length - 1; i >= 0; i--)
    {
        var tmp = arr[i];
        arr[i] = arr[0];
        arr[0] = tmp;
        MaxHeapify(arr, i, 0);
    }
}

var arr = File.ReadAllLines(@"C:\Users\Oliver\Desktop\rosalind_hs.txt")
    .Skip(1)
    .SelectMany(x => x.Split(' ').Select(int.Parse))
    .ToArray();

HeapSort(arr);

foreach (var number in arr)
    Console.Write($"{number} ");