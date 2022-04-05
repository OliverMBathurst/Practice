var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt");

static long GetSlopeTreeSum(string[] input, int xIncrement, int yIncrement)
{
    var treesEncountered = 0L;
    var xLength = input[0].Length;

    for (int x = 0, y = 0; y < input.Length; x += xIncrement, y += yIncrement)
        if (input[y][x < xLength ? x : x % xLength] == '#')
            treesEncountered++;

    return treesEncountered;
}

//Part 1
Console.WriteLine(GetSlopeTreeSum(lines, 3, 1));
//232

//Part 2
Console.WriteLine(
    GetSlopeTreeSum(lines, 1, 1) *
    GetSlopeTreeSum(lines, 3, 1) *
    GetSlopeTreeSum(lines, 5, 1) *
    GetSlopeTreeSum(lines, 7, 1) *
    GetSlopeTreeSum(lines, 1, 2)
);
//3952291680