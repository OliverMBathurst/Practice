var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt");

static bool IsAdjacent((int X, int Y) a, (int X, int Y) b)
{
    return (a.X == b.X && a.Y == b.Y)
        || (a.Y == b.Y && Math.Abs(a.X - b.X) == 1)
        || (a.X == b.X && Math.Abs(a.Y - b.Y) == 1)
        || (Math.Abs(a.X - b.X) == 1 && Math.Abs(a.Y - b.Y) == 1);
}

static HashSet<(int X, int Y)> GetVisitedPositions(string[] instructions, int knotCount)
{
    (int X, int Y)[] knots = Enumerable.Range(1, knotCount)
        .Select(x => (0, 0))
        .ToArray();

    var visitedPositionsTail = new HashSet<(int X, int Y)> { (0, 0) };
    foreach (var line in instructions)
    {
        var split = line.Split(" ");
        var places = int.Parse(split[1]);

        for (var i = 0; i < places; i++)
        {
            switch (split[0])
            {
                case "D":
                    knots[0].Y--;
                    break;
                case "U":
                    knots[0].Y++;
                    break;
                case "L":
                    knots[0].X--;
                    break;
                case "R":
                    knots[0].X++;
                    break;
            }

            for (var j = 1; j < knots.Length; j++)
            {
                if (IsAdjacent(knots[j], knots[j - 1]))
                    continue;

                if (knots[j].X < knots[j - 1].X)
                    knots[j].X++;
                else if (knots[j].X > knots[j - 1].X)
                    knots[j].X--;

                if (knots[j].Y < knots[j - 1].Y)
                    knots[j].Y++;
                else if (knots[j].Y > knots[j - 1].Y)
                    knots[j].Y--;
            }

            visitedPositionsTail.Add(knots[^1]);
        }
    }

    return visitedPositionsTail;
}

Console.WriteLine(GetVisitedPositions(lines, 2).Count); //6391
Console.WriteLine(GetVisitedPositions(lines, 10).Count); //2593