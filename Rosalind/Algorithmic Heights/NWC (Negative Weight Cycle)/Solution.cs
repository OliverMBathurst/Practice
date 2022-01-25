var graphs = File.ReadAllLines(@"C:\Users\Oliver\Desktop\rosalind_nwc.txt")
    .Skip(1)
    .Split(line => line.Count(c => c == ' ') == 1)
    .Select(graph =>
    (int.Parse(graph.First().Split(" ")[0]), graph.Skip(1).Select(edge =>
    {
        var parts = edge.Split(" ");
        return (u: int.Parse(parts[0]), v: int.Parse(parts[1]), w: int.Parse(parts[2]));
    })));

static bool HasBellmanFordNegativeCycle(List<(int u, int v, int w)> edges, IEnumerable<int> uniqueVertices)
{
    var dist = new Dictionary<int, double>();
    var prev = new Dictionary<int, int?>();

    foreach (var u in uniqueVertices)
    {
        dist[u] = double.PositiveInfinity;
        prev[u] = null;
    }

    var dummyNodeId = uniqueVertices.Count() + 1;
    foreach (var vertex in uniqueVertices)
        edges.Add((dummyNodeId, vertex, 0));

    dist[dummyNodeId] = 0;

    bool UpdateWithNegativeReturn((int u, int v, int w) edge)
    {
        var (u, v, w) = edge;

        var neg = dist[u] + w < dist[v];
        if (neg)
        {
            dist[v] = dist[u] + w;
            prev[v] = u;
        }

        return neg;
    }

    (uniqueVertices.Count() - 1).Repeat(() => edges.ForEach(edge => UpdateWithNegativeReturn(edge)));
    return edges.Any(UpdateWithNegativeReturn);
}

foreach (var (VertexCount, Edges) in graphs)
    Console.Write($"{(HasBellmanFordNegativeCycle(Edges.ToList(), Enumerable.Range(1, VertexCount)) ? 1 : -1)} ");
