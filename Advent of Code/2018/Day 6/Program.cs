var coordinates = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt")
    .Select(str =>
    {
        var split = str.Split(", ");
        return new { X = int.Parse(split[0]), Y = int.Parse(split[1]) };
    });

var areas = new Dictionary<(int X, int Y), int>();
var infinites = new HashSet<(int X, int Y)>();

int xMax = coordinates.Max(x => x.X), yMax = coordinates.Max(x => x.Y), partTwoArea = 0;
for (var i = -1; i <= xMax + 1; i++)
{
    for (var j = -1; j <= yMax + 1; j++)
    {
        var outOfBounds = i < 0 || j < 0 || i == xMax + 1 || j == yMax + 1;
        var distDict = coordinates
            .Select(c => new { Coordinate = c, Distance = Math.Abs(c.X - i) + Math.Abs(c.Y - j) })
            .ToDictionary(k => k.Coordinate, v => v.Distance);

        var minimumDist = distDict.OrderBy(x => x.Value).ToArray();
        if (!outOfBounds)
        {
            var sumOfDistances = minimumDist.Sum(x => x.Value);
            if (sumOfDistances < 10000)
            {
                partTwoArea++;
            }
        }

        if (minimumDist.Length >= 2 && minimumDist[0].Value == minimumDist[1].Value)
        {
            continue;
        }

        var tuple = (minimumDist[0].Key.X, minimumDist[0].Key.Y);
        if (outOfBounds)
        {
            infinites.Add(tuple);
            continue;
        }

        if (!infinites.Contains(tuple))
        {
            if (!areas.ContainsKey(tuple))
            {
                areas.Add(tuple, 0);
            }

            areas[tuple]++;
        }
    }
}

Console.WriteLine(areas.Where(x => !infinites.Contains(x.Key)).Max(x => x.Value)); //3238
Console.WriteLine(partTwoArea); //45046