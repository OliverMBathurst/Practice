var discs = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt").Select(line =>
{
    var split = line.Split("; ");
    var splitLeft = split[0].Split(" ");
    int discNumber = splitLeft[1][1] - '0', numPositions = int.Parse(splitLeft[3]), positionAtTimeZero = int.Parse(split[1].Split(".")[0].Split(" ")[^1]);
    return (
        DiscNumber: discNumber,
        NumberOfPositions: numPositions,
        PositionAtTimeZero: positionAtTimeZero
    );
}).ToDictionary(k => k.DiscNumber, v => v);

//Part 1
Console.WriteLine(GetFirstTimeOffset(discs));
//376777

//Part 2
discs.Add(discs.Count + 1, (discs.Count + 1, 11, 0));
Console.WriteLine(GetFirstTimeOffset(discs));
//3903937

static int GetFirstTimeOffset(IDictionary<int, (int DiscNumber, int NumberOfPositions, int PositionAtTimeZero)> discDict)
{
    var timeOffset = 0;
    while (Enumerable.Range(1, discDict.Count).Any(discNum => (discDict[discNum].PositionAtTimeZero + timeOffset + discNum) % discDict[discNum].NumberOfPositions != 0))
        timeOffset++;

    return timeOffset;
}