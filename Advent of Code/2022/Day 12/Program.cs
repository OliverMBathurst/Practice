var grid = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt")
    .Select(str => str.ToCharArray())
    .ToArray();

static (int X, int Y) FindC(char[][] grid, char target)
{
    int i = 0, j = 0;
    var sFound = false;
    while (!sFound)
    {
        if (grid[i][j] == target)
        {
            sFound = true;
        }
        else
        {
            if (j + 1 < grid[i].Length)
            {
                j++;
            }
            else
            {
                i++;
                j = 0;
            }
        }
    }

    return (j, i);
}

static List<(int X, int Y)> GetAllLowestPoints(char[][] grid)
{
    var lowest = new List<(int X, int Y)>();
    for (var y = 0; y < grid.Length; y++)
        for (var x = 0; x < grid[y].Length; x++)
            if (grid[y][x] == 'a' || grid[y][x] == 'S')
                lowest.Add((x, y));

    return lowest;
}

static bool CanVisit(int currentX, int currentY, int proposedX, int proposedY, char[][] grid)
{
    if (proposedX < 0 || proposedY < 0 || proposedY >= grid.Length || proposedX >= grid[proposedY].Length)
    {
        return false;
    }

    var maxElevation = char.ToUpper(grid[currentY][currentX] == 'S' 
        ? 'a' 
        : grid[currentY][currentX]) - 63;

    var proposedElevation = char.ToUpper(grid[proposedY][proposedX]) - 64;
    if (maxElevation < proposedElevation)
    {
        return false;
    }

    if (grid[proposedY][proposedX] == 'E')
    {
        if (grid[currentY][currentX] == 'z')
        {
            return true;
        }

        return false;
    }

    return true;
}

static List<(int X, int Y, int W)> GetVisitables(int x, int y, char[][] grid)
{
    var visitables = new List<(int X, int Y, int W)>();
    
    if (CanVisit(x, y, x - 1, y, grid))
        visitables.Add((x - 1, y, 1));

    if (CanVisit(x, y, x + 1, y, grid))
        visitables.Add((x + 1, y, 1));

    if (CanVisit(x, y, x, y - 1, grid))
        visitables.Add((x, y - 1, 1));

    if (CanVisit(x, y, x, y + 1, grid))
        visitables.Add((x, y + 1, 1));

    return visitables;
}

static IDictionary<(int X, int Y), double> GetDijkstraPathLengths(IDictionary<(int X, int Y), List<(int X, int Y, int W)>> V, IEnumerable<(int X, int Y)> uniqueVertices, (int X, int Y) S)
{
    var dist = new Dictionary<(int X, int Y), double>();
    var prev = new Dictionary<(int X, int Y), (int X, int Y)?>();

    foreach (var vertex in uniqueVertices)
    {
        dist[vertex] = double.PositiveInfinity;
        prev[vertex] = null;
    }

    dist[S] = 0;

    var Q = new List<(int X, int Y)>(uniqueVertices);
    while (Q.Count > 0)
    {
        var min = Q.OrderBy(x => dist[x]).First();
        var u = min;
        Q.Remove(min);

        if (!V.ContainsKey(u))
            continue;

        foreach (var (X, Y, W) in V[u].Where(t => Q.Contains((t.X, t.Y))))
        {
            var alt = dist[u] + W;
            if (alt < dist[(X, Y)])
            {
                dist[(X, Y)] = alt;
                prev[(X, Y)] = u;
            }
        }
    }

    return dist;
}

var visitables = new Dictionary<(int X, int Y), List<(int X, int Y, int W)>>();
for (var sI = 0; sI < grid.Length; sI++)
    for (var sJ = 0; sJ < grid[sI].Length; sJ++)
        visitables.Add((sJ, sI), GetVisitables(sJ, sI, grid));

var (sX, sY) = FindC(grid, 'S');
var (eX, eY) = FindC(grid, 'E');

var uniqueVertices = visitables.Select(x => x.Key);

Console.WriteLine(GetDijkstraPathLengths(visitables, uniqueVertices, (sX, sY))[(eX, eY)]); //447

var shortestPathFromAtoE = GetAllLowestPoints(grid)
    .AsParallel()
    .Select(tuple => GetDijkstraPathLengths(visitables, uniqueVertices, tuple)[(eX, eY)])
    .AsSequential()
    .OrderBy(x => x)
    .First();

Console.WriteLine(shortestPathFromAtoE); //446