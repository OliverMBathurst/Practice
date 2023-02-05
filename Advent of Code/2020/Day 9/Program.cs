var numbers = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt")
    .Select(long.Parse)
    .ToArray();

static int GetFirstInvalidNumberIndex(long[] arr, int preambleLength)
{
    var idx = preambleLength;
    while (idx < arr.Length)
    {
        var valid = false;
        for (var sI = idx - preambleLength; sI < idx; sI++)
        {
            for (var sJ = sI + 1; sJ < idx; sJ++)
            {
                if (arr[sI] != arr[sJ] && arr[sI] + arr[sJ] == arr[idx])
                {
                    valid = true;
                    break;
                }
            }
        }

        if (!valid)
        {
            return idx;
        }

        idx++;
    }

    return -1;
}

static long GetSumOfContiguousBounds(long[] numbers, int targetIdx)
{
    var idx = 0;
    while (idx < targetIdx)
    {
        if (numbers[idx] < numbers[targetIdx])
        {
            var sum = 0L;
            long? min = null, max = null;
            for (var i = idx; i < targetIdx; i++)
            {
                var val = numbers[i];
                if (max == null)
                {
                    max = val;
                } 
                else if (val > max)
                {
                    min ??= max;
                    max = val;
                } 
                else if (min == null || val < min)
                {
                    min = val;
                }

                sum += val;

                if (sum == numbers[targetIdx] && min.HasValue && max.HasValue)
                {
                    return min.Value + max.Value;
                }
                else if (sum > numbers[targetIdx])
                {
                    break;
                }
            }
        }

        idx++;
    }

    return 0L;
}

var indexOfFirstInvalid = GetFirstInvalidNumberIndex(numbers, 25);
Console.WriteLine(numbers[indexOfFirstInvalid]); //400480901
Console.WriteLine(GetSumOfContiguousBounds(numbers, indexOfFirstInvalid)); //67587168