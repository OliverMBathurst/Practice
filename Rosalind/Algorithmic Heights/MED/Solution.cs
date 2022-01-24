static T FindK<T>(T[] array, int k) where T : IComparable<T>
{
    var v = new Random().Next(array.Length);

    var pIdx = 0;
    for (var i = 0; i < array.Length; i++)
        if (i != v && array[v].CompareTo(array[i]) >= 0)
            pIdx++;

    if (pIdx != v)
    {
        var tmp = array[pIdx];
        array[pIdx] = array[v];
        array[v] = tmp;
    }

    int grIdx = pIdx + 1, lIdx = 0, eIdx = 0, SvStartIndex = pIdx;
    for (var i = grIdx; i < array.Length; i++)
    {
        if (array[i].CompareTo(array[pIdx]) <= 0)
        {
            for (var j = lIdx; j < pIdx; j++)
            {
                if (array[j].CompareTo(array[pIdx]) > 0)
                {
                    var tmp = array[i];
                    array[i] = array[j];
                    array[j] = tmp;

                    lIdx = j + 1;
                    break;
                }
            }
        }
    }

    for (var i = pIdx - 1; i >= 0; i--)
    {
        if (array[i].CompareTo(array[pIdx]) != 0)
        {
            var hasBefore = false;
            for (var j = eIdx; j < i; j++)
            {
                if (array[j].CompareTo(array[pIdx]) == 0)
                {
                    var tmp = array[i];
                    array[i] = array[j];
                    array[j] = tmp;
                    eIdx = j + 1;
                    SvStartIndex = i;

                    if (j != i - 1)
                        hasBefore = true;
                    break;
                }
            }

            if (!hasBefore)
                break;
        }
    }

    int SvL = pIdx - SvStartIndex + 1, SLL = SvStartIndex;

    int n;
    Range r;
    if (k < SLL)
    {
        r = new(0, SvStartIndex - 1);
        n = k;
    } 
    else if (k <= SLL + SvL)
    {
        return array[pIdx];
    }
    else
    {
        r = new(pIdx + 1, array.Length - 1);
        n = k - SvL - SLL;
    }

    QuickSort(array, r.Start.Value, r.End.Value);

    return array[r.Start.Value + n - 1];
}

static void QuickSort<T>(T[] A, int lo, int hi) where T : IComparable<T>
{
    if (lo >= 0 && hi >= 0 && lo < hi)
    {
        var p = Partition(A, lo, hi);
        QuickSort(A, lo, p);
        QuickSort(A, p + 1, hi);
    }
}

static int Partition<T>(T[] A, int lo, int hi) where T : IComparable<T>
{
    var pivot = A[(int)Math.Floor((double)((hi + lo) / 2))];
    int i = lo - 1, j = hi + 1;

    while (true)
    {
        do
        {
            i++;
        } while (A[i].CompareTo(pivot) < 0);

        do
        {
            j--;
        } while (A[j].CompareTo(pivot) > 0);

        if (i >= j)
            return j;

        var tmp = A[j];
        A[j] = A[i];
        A[i] = tmp;
    }
}

var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\rosalind_med.txt").ToArray();
var arr = lines[1].Split(' ').Select(int.Parse).ToArray();
Console.WriteLine(FindK(arr, int.Parse(lines[2])));