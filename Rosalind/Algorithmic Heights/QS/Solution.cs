static void QuickSort<T>(T[] A, int lo, int hi) where T : IComparable<T>
{
    if (lo >= hi || lo < 0)
        return;

    var p = Partition(A, lo, hi);
    QuickSort(A, lo, p - 1);
    QuickSort(A, p + 1, hi);
}

static int Partition<T>(T[] A, int lo, int hi) where T : IComparable<T>
{
    var pivot = A[hi];
    var i = lo - 1;

    for (var j = lo; j < hi; j++) 
    { 
        if (A[j].CompareTo(pivot) <= 0)
        {
            i++;
            var tmp = A[j];
            A[j] = A[i];
            A[i] = tmp;
        }
    }

    i++;
    var temp = A[hi];
    A[hi] = A[i];
    A[i] = temp;
    return i;
}

var numbers = File.ReadAllLines(@"C:\Users\Oliver\Desktop\rosalind_qs.txt")
    .Skip(1)
    .SelectMany(x => x.Split(' ').Select(int.Parse))
    .ToArray();

QuickSort(numbers, 0, numbers.Length - 1);
foreach (var num in numbers)
    Console.Write($"{num} ");