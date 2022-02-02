//Part 1
var input = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt")
    .Select(x => x.Split(" through "))
    .Select(x =>
    {
        var endCoordinates = x[^1].Split(',').Select(int.Parse).ToArray();
        var lhs = x[0].Split(' ');
        var startCoordinates = lhs[^1].Split(',').Select(int.Parse).ToArray();

        return new
        {
            End = (X: endCoordinates[1], Y: endCoordinates[0]),
            Start = (X: startCoordinates[1], Y: startCoordinates[0]),
            Instruction = lhs.Length == 2 ? "toggle" : lhs[1]
        };
    });

var lights = new bool[1000][];
for (var i = 0; i < lights.Length; i++)
    lights[i] = new bool[1000];

foreach (var line in input)
    for (var y = line.Start.Y; y <= line.End.Y; y++)
        for (var x = line.Start.X; x <= line.End.X; x++)
            lights[y][x] = line.Instruction == "toggle" ? !lights[y][x] : line.Instruction == "on";

Console.WriteLine(lights.Sum(x => x.Count(t => t)));
//400410

//Part 2
var lightsPartTwo = new int[1000][];
for (var lIdx = 0; lIdx < lightsPartTwo.Length; lIdx++)
    lightsPartTwo[lIdx] = new int[1000];

foreach (var line in input)
    for (var y = line.Start.Y; y <= line.End.Y; y++)
        for (var x = line.Start.X; x <= line.End.X; x++)
            lightsPartTwo[y][x] += line.Instruction == "toggle" ? 2 : line.Instruction == "on" ? 1 : (lightsPartTwo[y][x] == 0 ? 0 : -1);

Console.WriteLine(lightsPartTwo.Sum(x => x.Sum()));
//15343601