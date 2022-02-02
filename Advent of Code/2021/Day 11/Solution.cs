//Part 1
var input = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt")
    .Where(x => !string.IsNullOrWhiteSpace(x))
    .Select(x => x.Select(c => int.Parse(c.ToString())).ToArray())
    .ToArray();

static IEnumerable<(int x, int y)> GetAllSurroundingIndices(int[][] structure, int x, int y)
{
    if (y + 1 < structure.Length)
        yield return (x, y + 1);

    if (y - 1 >= 0)
    {
        yield return (x, y - 1);
        if (x + 1 < structure[y - 1].Length)
            yield return (x + 1, y - 1);

        if (x - 1 >= 0)
            yield return ((x - 1, y - 1));
    }

    if (x + 1 < structure[y].Length)
    {
        yield return (x + 1, y);
        if (y + 1 < structure.Length)
            yield return (x + 1, y + 1);
    }

    if (x - 1 >= 0)
    {
        yield return (x - 1, y);
        if (y + 1 < structure.Length)
            yield return (x - 1, y + 1);
    }
}


static int Flash(int[][] structure, int x, int y)
{
    var flashes = 1;
    structure[y][x] = 0;

    foreach (var index in GetAllSurroundingIndices(structure, x, y))
    {
        if (structure[index.y][index.x] != 0)
            structure[index.y][index.x]++;

        if (structure[index.y][index.x] > 9)
            flashes += Flash(structure, index.x, index.y);
    }

    return flashes;
}

static int[][] GetNewInput(int[][] structure)
{
    var newArr = new int[structure.Length][];

    for (var i = 0; i < newArr.Length; i++)
    {
        newArr[i] = new int[structure[i].Length];
        for (var j = 0; j < newArr[i].Length; j++)
        {
            newArr[i][j] = structure[i][j];
        }
    }

    return newArr;
}

static int Step(int[][] structure)
{
    var flashes = 0;

    for (var yIdx = 0; yIdx < structure.Length; yIdx++)
        for (var xIdx = 0; xIdx < structure[yIdx].Length; xIdx++)
            structure[yIdx][xIdx]++;

    for (var yIdx = 0; yIdx < structure.Length; yIdx++)
        for (var xIdx = 0; xIdx < structure[yIdx].Length; xIdx++)
            if (structure[yIdx][xIdx] > 9)
                flashes += Flash(structure, xIdx, yIdx);

    return flashes;
}

var stepOne = GetNewInput(input);
Console.WriteLine(Enumerable.Range(0, 100).Sum(x => Step(stepOne)));
//1603

//Part 2
var stepTwo = GetNewInput(input);
var foundIndex = false;
var steps = 0;

while (!foundIndex)
{
    Step(stepTwo);
    steps++;
    if (stepTwo.All(row => row.All(c => c == 0)))
        foundIndex = true;
}

Console.WriteLine(steps);
//222