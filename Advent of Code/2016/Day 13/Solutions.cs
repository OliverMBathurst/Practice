
var distanceDict = DijkstraSolve(0..100, 0..100, 1350);

//Part 1
Console.WriteLine(distanceDict[(31, 39)]);
//92

//Part 2
Console.WriteLine(distanceDict.Count(kvp => kvp.Value <= 50));
//124

static IDictionary<(int x, int y), double> DijkstraSolve(Range yRange, Range xRange, int specialNumber)
{
    var nonWallCoordinates = Enumerable.Range(yRange.Start.Value, yRange.End.Value)
        .SelectMany(y => Enumerable.Range(xRange.Start.Value, xRange.End.Value).Where(x => !IsWall(x, y, specialNumber)).Select(x => (x, y)))
        .ToHashSet();

    var dist = new Dictionary<(int x, int y), double>();
    var prev = new Dictionary<(int x, int y), (int x, int y)?>();

    foreach (var (x, y) in nonWallCoordinates)
    {
        dist.Add((x, y), double.PositiveInfinity);
        prev.Add((x, y), null);
    }

    dist[(1, 1)] = 0;

    var Q = new List<(int x, int y)>(nonWallCoordinates);
    while (Q.Count > 0)
    {
        var min = Q.MinBy(x => dist[x]);
        Q.Remove(min);

        foreach (var v in new (int x, int y)[] { (min.x + 1, min.y), (min.x - 1, min.y), (min.x, min.y + 1), (min.x, min.y - 1) }.Where(x => nonWallCoordinates.Contains(x)))
        {
            if (dist[v] > dist[min] + 1)
            {
                dist[v] = dist[min] + 1;
                prev[v] = min;
            }
        }
    }

    return dist;
}

static bool IsWall(int x, int y, int specialNumber)
{
    double product = (x * x) + (3 * x) + (2 * x * y) + y + (y * y) + specialNumber;

    var power = 0;
    while (product > Math.Pow(2, power + 1))
        power++;

    var ones = 0;
    while (power >= 0)
    {
        var i = Math.Pow(2, power);
        if (product >= i)
        {
            product -= i;
            ones++;
        }

        if (product == 0D)
            break;

        power--;
    }

    return ones % 2 != 0;
}