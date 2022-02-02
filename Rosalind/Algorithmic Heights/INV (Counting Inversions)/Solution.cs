static T[] MergeSort<T>(T[] sequence, ref long inversions) where T : IComparable<T>
{
    var count = sequence.Length;
    if (count == 1)
        return sequence;

    var halfCount = count / 2;
    int lCount = halfCount, rCount = halfCount;

    if (count % 2 != 0)
        rCount = count - halfCount;

    T[] left = new T[lCount], right = new T[rCount];

    for (var i = 0; i < left.Length; i++)
        left[i] = sequence[i];

    for (int j = left.Length, rIdx = 0; rIdx < right.Length; j++, rIdx++)
        right[rIdx] = sequence[j];

    return Merge(MergeSort(left, ref inversions), MergeSort(right, ref inversions), ref inversions);
}

static T[] Merge<T>(T[] left, T[] right, ref long inversions) where T : IComparable<T>
{
    int lIdx = 0, rIdx = 0, resIdx = 0;
    var resultArr = new T[left.Length + right.Length];

    void SetAndIncrement(T[] array, ref int idx)
    {
        resultArr[resIdx] = array[idx];
        resIdx++;
        idx++;
    }

    while (lIdx < left.Length && rIdx < right.Length)
    {
        if (left[lIdx].CompareTo(right[rIdx]) <= 0)
        {
            SetAndIncrement(left, ref lIdx);
        }
        else
        {
            SetAndIncrement(right, ref rIdx);
            inversions += left.Length - lIdx;
        } 
    }

    while (lIdx < left.Length)
        SetAndIncrement(left, ref lIdx);

    while (rIdx < right.Length)
        SetAndIncrement(right, ref rIdx);

    return resultArr;
}

var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\rosalind_inv.txt");
var arr = lines.Skip(1)
    .SelectMany(x => x.Split(' ').Select(int.Parse))
    .ToArray();

var inversions = 0L;
MergeSort(arr, ref inversions);
Console.WriteLine(inversions);