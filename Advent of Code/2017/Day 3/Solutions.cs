//Part 1
static int GetManhattanDistance(int n)
{
    if (n == 1)
        return 0;

    int prevMax = 1, prevStep = 0, rIdx = 1;
    while (true)
    {
        int step = prevStep + 2, min = prevMax + prevStep + 1, max = min + (3 * step);

        if (n == min || n == max)
            return rIdx;

        if (n >= min && n <= max)
            return new int[] { min, min + step, max - step, max }.Min(x => Math.Abs(n - x)) + rIdx;

        if (n > max && n < (max + step + 1))
            return new int[] { Math.Abs(n - max), Math.Abs(n - (max + step + 1)) }.Min() + rIdx;

        prevMax = max;
        prevStep = step;
        rIdx++;
    }
}

Console.WriteLine(GetManhattanDistance(312051));
//430