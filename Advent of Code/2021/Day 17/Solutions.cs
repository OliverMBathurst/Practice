var input = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt")[0]
    .Split(": ")[1].Split(", ");

static IEnumerable<(int X, int Y, int MaxY)> GetMaxY(int maxX, int minY, int minX, int maxY) 
{
    for (var y = -300; y < 300; y++)
        for (var x = -300; x < 300; x++)
            if (GetYSteps(x, y, maxX, minY, minX, maxY, out var max))
                yield return (x, y, max);
}

static bool GetYSteps(int x, int y, int maxX, int minY, int minX, int maxY, out int max)
{
    int probeX = 0, probeY = 0;
    max = 0;

    while (probeX <= maxX && probeY >= minY)
    {
        probeX += x;
        probeY += y;

        if (x > 0)
            x--;
        else if (x < 0)
            x++;

        y--;

        if (probeY > max)
            max = probeY;

        if (probeX >= minX && probeX <= maxX && probeY >= minY && probeY <= maxY)
            return true;
    }

    return false;
}

string[] splitX = input[0].Split("x=")[1].Split(".."), splitY = input[1].Split("y=")[1].Split("..");

int y1 = int.Parse(splitY[0]), y2 = int.Parse(splitY[1]), x1 = int.Parse(splitX[0]), x2 = int.Parse(splitX[1]);

int yMax = Math.Max(y1, y2), xMax = Math.Max(x1, x2), yMin = Math.Min(y1, y2), xMin = Math.Min(x1, x2);

var result = GetMaxY(xMax, yMin, xMin, yMax).OrderByDescending(x => x.MaxY);

//Part 1
Console.WriteLine(result.First().MaxY);
//4753

//Part 2
Console.WriteLine(result.Count());
//1546