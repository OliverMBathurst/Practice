var polymerString = string.Join(string.Empty, File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt"));

static int GetPolymerLength(char?[] polymer) {
    int iIdx = 0, jIdx = 1;
    while (jIdx < polymer.Length)
    {
        char? i = polymer[iIdx], j = polymer[jIdx];
        if (char.ToLower(i!.Value) == char.ToLower(j!.Value) && i.Value != j.Value)
        {
            polymer[iIdx] = null;
            polymer[jIdx] = null;

            if (iIdx > 0)
                iIdx--;

            if (!polymer[iIdx].HasValue)
            {
                while (iIdx > 0 && !polymer[iIdx].HasValue)
                    iIdx--;
                while (!polymer[iIdx].HasValue)
                    iIdx++;
            }
        }
        else
        {
            iIdx++;
            while (!polymer[iIdx].HasValue)
                iIdx++;
        }

        jIdx++;
        if (iIdx == jIdx)
            jIdx++;
    }

    return polymer.Count(c => c.HasValue);
}

Console.WriteLine(GetPolymerLength(polymerString.Select(c => (char?)c).ToArray())); //9386
Console.WriteLine(polymerString.Select(c => char.ToLower(c)).Distinct().Select(target => GetPolymerLength(polymerString.Where(c => char.ToLower(c) != target).Select(c => (char?)c).ToArray())).Min()); //4876