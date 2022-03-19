var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt")
    .Select(line => line.Split(',').Select(ins => (dir: ins[0], val: int.Parse(new string(ins.Skip(1).ToArray())))))
    .ToArray();

static HashSet<(int x, int y)> GetAllUniqueVisitedCoordinates(IEnumerable<(char dir, int val)> line)
{
    var ins = new HashSet<(int x, int y)>();
    int sX = 0, sY = 0;

    foreach (var (dir, val) in line)
    {
        switch (dir)
        {
            case 'R':
                for (var i = sX + 1; i <= sX + val; i++)
                    ins.Add((x: i, y: sY));
                sX += val;
                break;
            case 'L':
                for (var i = sX - 1; i >= sX - val; i--)
                    ins.Add((x: i, y: sY));
                sX -= val;
                break;
            case 'U':
                for (var i = sY + 1; i <= sY + val; i++)
                    ins.Add((x: sX, y: i));
                sY += val;
                break;
            case 'D':
                for (var i = sY - 1; i >= sY - val; i--)
                    ins.Add((x: sX, y: i));
                sY -= val;
                break;
        }
    }

    return ins;
}

var results = lines.Select(GetAllUniqueVisitedCoordinates);
var intersections = new HashSet<(int x, int y)>(results.First());

foreach (var coordinateSet in results.Skip(1))
    intersections.IntersectWith(coordinateSet);

//Part 1
Console.WriteLine(intersections.Min(co => Math.Abs(co.x) + Math.Abs(co.y)));
//1519

//Part 2
static Dictionary<(int x, int y), int> GetStepsToIntersections(IEnumerable<(char dir, int val)> instructions, HashSet<(int x, int y)> intersections)
{
    int sX = 0, sY = 0, stepsTotal = 0;
    var steps = new Dictionary<(int x, int y), int>();

    void AddSteps((int x, int y) step)
    {
        stepsTotal++;
        if (intersections.Contains(step) && !steps.ContainsKey(step))
            steps.Add(step, stepsTotal);
    }

    foreach (var (dir, val) in instructions)
    {
        switch (dir)
        {
            case 'R':
                for (var i = sX + 1; i <= sX + val; i++)
                    AddSteps((i, sY));
                sX += val;
                break;
            case 'L':
                for (var i = sX - 1; i >= sX - val; i--)
                    AddSteps((i, sY));
                sX -= val;
                break;
            case 'U':
                for (var i = sY + 1; i <= sY + val; i++)
                    AddSteps((sX, i));
                sY += val;
                break;
            case 'D':
                for (var i = sY - 1; i >= sY - val; i--)
                    AddSteps((sX, i));
                sY -= val;
                break;
        }
    }

    return steps;
}

var minSum = lines.SelectMany(instructions => GetStepsToIntersections(instructions, intersections))
    .GroupBy(x => x.Key)
    .Select(x => x.Sum(x => x.Value))
    .Min();

Console.WriteLine(minSum);
//14358