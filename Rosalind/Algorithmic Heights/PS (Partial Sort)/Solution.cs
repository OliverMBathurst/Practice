var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\rosalind_ps.txt");
var numbers = lines[1].Split(" ").Select(int.Parse).ToArray();
var k = int.Parse(lines[2]);
PartialSort(numbers, 0, numbers.Length - 1, k);

Console.WriteLine(string.Join(" ", numbers[0..k]));

static void PartialSort<T>(T[] A, int lo, int hi, int k) where T : IComparable<T>
{
    if (lo >= hi || lo < 0)
        return;

    var p = Partition(A, lo, hi);
    PartialSort(A, lo, p - 1, k);
    if (p < k - 1)
        PartialSort(A, p + 1, hi, k);
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
            (A[j], A[i]) = (A[i], A[j]);
        }
    }

    i++;
    (A[hi], A[i]) = (A[i], A[hi]);
    return i;
}