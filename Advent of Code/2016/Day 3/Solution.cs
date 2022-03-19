var triangles = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt")
    .Select(line =>
    {
        var split = line.Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
        return new int[] { int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2]) };
    }).ToArray();

static bool IsValidTriangle(int a, int b, int c) => a + b > c && a + c > b && c + b > a;

//Part 1
Console.WriteLine(triangles.Count(t => IsValidTriangle(t[0], t[1], t[2])));
//862

//Part 2
static int GetValidTriangles(int[][] triangleLines)
{
    var validTriangles = 0;
    for (var i = 0; i + 2 < triangleLines.Length; i += 3)
        for (var j = 0; j < 3; j++)
            if (IsValidTriangle(triangleLines[i][j], triangleLines[i + 1][j], triangleLines[i + 2][j]))
                validTriangles++;

    return validTriangles;
}

Console.WriteLine(GetValidTriangles(triangles));
//1577