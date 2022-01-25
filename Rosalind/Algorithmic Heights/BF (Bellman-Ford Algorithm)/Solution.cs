var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\rosalind_bf.txt");

var visitables = lines.Skip(1).Select(x =>
{
    var parts = x.Split(" ");
    return (A: int.Parse(parts[0]), B: int.Parse(parts[1]), W: int.Parse(parts[2]));
}).ToDictionary(
    elem => elem.A,
    (elem, _) => new List<(int V, int LUV)> { (V: elem.B, LUV: elem.W) }, 
    (elem, _, val) => val.Add((V: elem.B, LUV: elem.W)));

static void PrintBellmanFordPathLengths(IDictionary<int, List<(int V, int LUV)>> G, IEnumerable<int> uniqueVertices)
{
    var dist = new Dictionary<int, double>();
    var prev = new Dictionary<int, int?>();

    foreach (var u in uniqueVertices)
    {
        dist[u] = double.PositiveInfinity;
        prev[u] = null;
    }

    dist[1] = 0;

    void Update(int u, (int V, int LUV) edge) => dist[edge.V] = Math.Min(dist[edge.V], dist[u] + edge.LUV);

    for (var i = 0; i < uniqueVertices.Count() - 1; i++)
        foreach (var kvp in G)
            foreach (var edge in kvp.Value)
                Update(kvp.Key, edge);

    foreach (var length in dist.OrderBy(x => x.Key).Select(x => x.Value))
        Console.Write($"{(length == double.PositiveInfinity ? "x" : length)} ");
}

PrintBellmanFordPathLengths(visitables, Enumerable.Range(1, int.Parse(lines.First().Split(" ")[0])));