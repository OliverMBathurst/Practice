//Part 1
using System.Diagnostics.CodeAnalysis;

var input = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt");

var coordinates = input.TakeWhile(x => !string.IsNullOrWhiteSpace(x))
    .Select(x =>
    {
        var split = x.Split(',');
        return new Point { X = int.Parse(split[0]), Y = int.Parse(split[1]) };
    })
    .ToArray();

foreach (var instruction in input.SkipWhile(x => !string.IsNullOrWhiteSpace(x)).Where(x => !string.IsNullOrWhiteSpace(x)))
{
    var split = instruction.Split('=');
    var value = int.Parse(split[1]);
    var xOrY = split[0][^1];

    foreach (var coord in coordinates)
        if (xOrY == 'x' && coord.X >= value)
            coord.X -= 2 * (coord.X - value);
        else if (xOrY == 'y' && coord.Y >= value)
            coord.Y -= 2 * (coord.Y - value);

    int minBoundX = coordinates.Min(x => x.X), minBoundY = coordinates.Min(x => x.Y);

    foreach (var coord in coordinates)
    {
        coord.Y = Math.Abs(minBoundY - coord.Y);
        coord.X = Math.Abs(minBoundX - coord.X);
    }

    coordinates = coordinates.DistinctBy(x => (x.X, x.Y)).ToArray();

    //Part 1
    Console.WriteLine("Instruction: " + instruction + " Count: " + coordinates.Length);
    //755
}

//Part 2
int maxY = coordinates.Max(x => x.Y) + 1, maxX = coordinates.Max(x => x.X) + 1;
var grid = new char[maxY][];
for (var i = 0; i < grid.Length; i++)
{
    grid[i] = new char[maxX];
    for (var j = 0; j < grid[i].Length; j++)
        grid[i][j] = ' ';
}

foreach (var coord in coordinates)
    grid[coord.Y][coord.X] = '#';

for (var rowIdx = 0; rowIdx < grid.Length; rowIdx++)
{
    for (var col = 0; col < grid[rowIdx].Length; col++)
        Console.Write(grid[rowIdx][col]);
    Console.WriteLine();
}
    
//BLKJRBAG

class Point
{
    public int X { get; set; }
    public int Y { get; set; }
}