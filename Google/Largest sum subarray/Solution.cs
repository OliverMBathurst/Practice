static long Kadane(int[] arr)
{
    long max = 0L, maxCurr = 0L;
    foreach (var integer in arr)
    {
        maxCurr += integer;
        if (maxCurr < 0)
            maxCurr = 0;

        if (maxCurr > max)
            max = maxCurr;
    }

    return max;
}

Console.Write(Kadane(new[] { -4, 2, -5, 1, 2, 3, 6, -5, 1 }));