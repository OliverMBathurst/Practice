var compressedLines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt").SelectMany(l => l).ToArray();

//Part 1
Console.WriteLine(GetDecompressedLineLength(compressedLines, false));
//115118

//Part 2
Console.WriteLine(GetDecompressedLineLength(compressedLines, true));
//11107527530

static long GetDecompressedLineLength(char[] input, bool versionTwo)
{
    var idx = 0;
    var totalLength = 0L;

    while (idx < input.Length)
    {
        if (input[idx] == '(')
        {
            var endIndex = idx + 1;
            while (input[endIndex] != ')')
                endIndex++;

            var split = new string(input[(idx + 1)..endIndex]).Split("x");
            var (numberCharacters, repeatCount) = (int.Parse(split[0]), int.Parse(split[1]));
            totalLength += !versionTwo
                ? numberCharacters * repeatCount
                : GetDecompressedLineLength(input[(endIndex + 1)..(endIndex + 1 + numberCharacters)], versionTwo) * repeatCount;

            idx = endIndex + 1 + numberCharacters;
            continue;
        }

        totalLength++;
        idx++;
    }

    return totalLength;
}