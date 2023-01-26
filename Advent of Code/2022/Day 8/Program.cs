var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt");

var grid = lines.Select(x => x.Select(c => c - '0').ToArray()).ToArray();
var visibleCount = (grid[0].Length + grid.Length - 2) * 2;

static (int, bool) Search(int x, int y, int i, int[][] grid, Func<int, bool> iterationCondition, Func<int, int> postIterationAction, Func<int, int, int, int> gridAccess) 
{
    var scenicScore = 0;

    while (iterationCondition(i))
    {
        if (gridAccess(i, x, y) >= grid[y][x])
        {
            scenicScore++;
            return (scenicScore, false);
        }
        else
        {
            scenicScore++;
        }

        i = postIterationAction(i);
    }

    return (scenicScore, true);
}

var maxScenicScore = 0;
for (var i = 1; i < grid.Length - 1; i++)
{
    for (var j = 1; j < grid[0].Length - 1; j++)
    {
        var scores = new List<(int ScenicScore, bool Visible)> {
            Search(j, i, i - 1, grid, ic => ic >= 0, pia => pia - 1, (ia, xa, _) => grid[ia][xa]),
            Search(j, i, i + 1, grid, ic => ic < grid.Length, pia => pia + 1, (ia, xa, _) => grid[ia][xa]),
            Search(j, i, j - 1, grid, ic => ic >= 0, pia => pia - 1, (ia, _, ya) => grid[ya][ia]),
            Search(j, i, j + 1, grid, ic => ic < grid.Length, pia => pia + 1, (ia, _, ya) => grid[ya][ia])
        };

        if (scores.Any(s => s.Visible))
        {
            visibleCount++;
        }

        var max = scores.Select(x => x.ScenicScore).Aggregate((a, b) => a * b);
        if (max > maxScenicScore)
        {
            maxScenicScore = max;
        }
    }
}

Console.WriteLine(visibleCount); //1803
Console.WriteLine(maxScenicScore); //268912