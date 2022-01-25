var numbers = File.ReadAllLines(@"C:\Users\Oliver\Desktop\rosalind_par.txt")
    .Skip(1)
    .SelectMany(str => str.Split(' ').Select(int.Parse))
    .ToArray();

static void TwoWayPartition<T>(T[] array) where T : IComparable<T>
{
    if (array.Length == 1)
        return;

    var pIdx = 0;
    for (var i = 0; i < array.Length; i++)
        if (array[0].CompareTo(array[i]) > 0)
            pIdx++;

    if (pIdx == 0)
        return;

    var tmp = array[pIdx];
    array[pIdx] = array[0];
    array[0] = tmp;

    var gIdx = pIdx + 1;
    for (var j = 0; j < pIdx; j++)
    {
        if (array[j].CompareTo(array[pIdx]) > 0)
        {
            for (var k = gIdx; k < array.Length; k++)
            {
                if (array[k].CompareTo(array[pIdx]) <= 0)
                {
                    var temp = array[k];
                    array[k] = array[j];
                    array[j] = temp;
                    gIdx = k + 1;
                    break;
                }
            }
        }
    }
}

TwoWayPartition(numbers);
foreach (var number in numbers)
    Console.Write($"{number} ");