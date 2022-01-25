static T[] MergeTwoSortedArrays<T>(T[] arrOne, T[] arrTwo) where T : IComparable<T>
{
    int arrOneIdx = 0, arrTwoIdx = 0, length = arrOne.Length + arrTwo.Length;
    var resultantArr = new T[length];

    for (var i = 0; i < length; i++)
    {
        if (arrOneIdx == arrOne.Length)
        {
            for (var j = arrTwoIdx; j < arrTwo.Length; j++)
            {
                resultantArr[i] = arrTwo[j];
                i++;
            }
            return resultantArr;
        }
        
        if (arrTwoIdx == arrTwo.Length)
        {
            for (var j = arrOneIdx; j < arrOne.Length; j++)
            {
                resultantArr[i] = arrOne[j];
                i++;
            }
            return resultantArr;
        }

        if (arrOne[arrOneIdx].CompareTo(arrTwo[arrTwoIdx]) == -1)
        {
            resultantArr[i] = arrOne[arrOneIdx];
            arrOneIdx++;
        }
        else
        {
            resultantArr[i] = arrTwo[arrTwoIdx];
            arrTwoIdx++;
        }
    }

    return resultantArr;
}

var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\rosalind_mer.txt").ToArray();
var sorted = MergeTwoSortedArrays(lines[1].Split(" ").Select(int.Parse).ToArray(), lines[3].Split(" ").Select(int.Parse).ToArray());

foreach (var num in sorted)
    Console.Write($"{num} ");