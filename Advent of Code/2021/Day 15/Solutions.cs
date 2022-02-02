var input = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt")
        .Where(x => !string.IsNullOrWhiteSpace(x))
        .Select(row => row.Select(r => int.Parse(r.ToString())).ToArray())
        .ToArray();

//Part 1
var nodeDict = new Dictionary<(int X, int Y), TreeNode>();

for (var y = 0; y < input.Length; y++)
{
    for (var x = 0; x < input[y].Length; x++)
    {
        var nn = new TreeNode { Value = input[y][x], Coordinates = (x, y) };

        if (y - 1 >= 0)
            nodeDict[(x, y - 1)].Left.Add(nn);

        if (x - 1 >= 0)
            nodeDict[(x - 1, y)].Right.Add(nn);


        nodeDict[(x, y)] = nn;
    }
}

static int Cost(
    TreeNode node, 
    TreeNode lastNode,
    IDictionary<TreeNode, int> costMappings,
    IDictionary<(int X, int Y), TreeNode> nodeMappings)
{
    if (node == lastNode)
        return node.Value;

    var correctedNodeValue = node.Coordinates == (0, 0) ? 0 : node.Value;
    var paths = node.Right.Concat(node.Left);

    var costs = new List<int>();
    foreach (var path in paths)
    {
        var alreadyCalculated = costMappings.ContainsKey(path);
        var cost = alreadyCalculated
            ? costMappings[path]
            : Cost(path, lastNode, costMappings, nodeMappings);

        if (!alreadyCalculated)
        {
            costMappings.Add(path, cost);
        }

        costs.Add(cost);
    }

    return costs.Any() ? costs.Min(x => x) + correctedNodeValue : correctedNodeValue;
}

static List<TreeNode> GetVisited(TreeNode node, IDictionary<TreeNode, int> costDictionary)
{
    var traversed = false;
    var currNode = node;
    var visited = new List<TreeNode>();

    while (!traversed)
    {
        var r = costDictionary.FirstOrDefault(x => x.Key.Coordinates == (currNode.Coordinates.X + 1, currNode.Coordinates.Y));
        var l = costDictionary.FirstOrDefault(x => x.Key.Coordinates == (currNode.Coordinates.X, currNode.Coordinates.Y + 1));
        var above = costDictionary.FirstOrDefault(x => x.Key.Coordinates == (currNode.Coordinates.X, currNode.Coordinates.Y - 1));

        if (r.Key == null && l.Key == null)
            traversed = true;

        if (!traversed)
        {
            var min = new[] { r, l, above }
                .Where(x => x.Key != null && !visited.Contains(x.Key))
                .OrderBy(x => x.Value);

            currNode = min.First().Key;
        }

        visited.Add(currNode);
    }

    return visited;
}

static void PrintGrid(int[][] input, List<TreeNode> nodes)
{
    for (var i = 0; i < input.Length; i++)
    {
        for (var j = 0; j < input[i].Length; j++)
        {
            var matching = nodes.FirstOrDefault(x => x.Coordinates == (j, i));
            Console.Write(matching != null ? " " : input[i][j]);
        }
        Console.WriteLine();
    }
}

TreeNode first = nodeDict[(0, 0)], last = nodeDict[(input[^1].Length - 1, input.Length - 1)];
var costDict = new Dictionary<TreeNode, int>();
var c = Cost(first, last, costDict, nodeDict);
Console.WriteLine(c);


PrintGrid(input, GetVisited(first, costDict));

class TreeNode : IEquatable<TreeNode>
{

    public int Value { get; set; }

    public (int X, int Y) Coordinates { get; set; }

    public List<TreeNode> Right { get; } = new List<TreeNode>();

    public List<TreeNode> Left { get; } = new List<TreeNode>();

    public bool Equals(TreeNode? other)
    {
        return other != null && other.Coordinates.Equals(Coordinates) && other.Value == Value;
    }
}