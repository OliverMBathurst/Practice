var banks = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt")
    .Select(x => x.Split("\t").Select(int.Parse))
    .First()
    .ToArray();

//Part 1
Console.WriteLine(GetCycles(banks));
//12841

//Part 2
Console.WriteLine(GetCycles(banks));
//8038

static int GetCycles(int[] banks)
{
    var results = new HashSet<string> { string.Join(',', banks.Select(b => b.ToString())) };
    var cycles = 0;

    while (true)
    {
        var largestIdx = 0;
        for (var i = 1; i < banks.Length; i++)
            if (banks[i] > banks[largestIdx])
                largestIdx = i;

        var tmpBlocks = banks[largestIdx];
        banks[largestIdx] = 0;

        var nIdx = largestIdx + 1;
        while (tmpBlocks > 0)
        {
            if (nIdx == banks.Length)
                nIdx = 0;

            banks[nIdx]++;

            tmpBlocks--;
            nIdx++;
        }

        cycles++;

        if (!results.Add(string.Join(',', banks.Select(b => b.ToString()))))
            return cycles;
    }
}