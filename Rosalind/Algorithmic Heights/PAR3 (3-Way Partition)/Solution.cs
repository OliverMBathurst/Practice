var numbers = File.ReadAllLines(@"C:\Users\Oliver\Desktop\rosalind_par3.txt")
    .Skip(1)
    .SelectMany(x => x.Split(' ').Select(int.Parse))
    .ToArray();

static void ThreeWayPartition<T>(T[] array) where T : IComparable<T>
{
    if (array.Length == 0 || array.Length == 1)
        return;

    var pIdx = 0;
    for (var i = 1; i < array.Length; i++)
        if (array[0].CompareTo(array[i]) >= 0)
            pIdx++;

    if (pIdx == 0)
        return;

    var tmp = array[pIdx];
    array[pIdx] = array[0];
    array[0] = tmp;

    var gIdx = pIdx + 1;
    for (var i = 0; i < pIdx; i++)
    {
        if (array[i].CompareTo(array[pIdx]) > 0)
        {
            for (var j = gIdx; j < array.Length; j++)
            {
                if (array[j].CompareTo(array[pIdx]) <= 0)
                {
                    var temp = array[j];
                    array[j] = array[i];
                    array[i] = temp;
                    gIdx = j + 1;
                    break;
                }
            }
        }
    }

    var fdIdx = 0;
    for (var i = pIdx - 1; i >= 0; i--)
    {
        if (array[i].CompareTo(array[pIdx]) < 0)
        {
            var hasBefore = false;
            for (var j = fdIdx; j < i; j++) 
            {
                if (array[j].CompareTo(array[pIdx]) == 0)
                {
                    var tmpTwo = array[j];
                    array[j] = array[i];
                    array[i] = tmpTwo;
                    fdIdx = j + 1;

                    if (j != i - 1)
                        hasBefore = true;

                    break;
                }
            }

            if (!hasBefore)
                break;
        }
    }
}

ThreeWayPartition(numbers);
foreach (var number in numbers)
    Console.Write($"{number} ");