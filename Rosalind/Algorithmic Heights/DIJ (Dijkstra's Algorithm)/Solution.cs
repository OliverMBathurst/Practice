var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\rosalind_dij.txt");

var visitables = lines.Skip(1).Select(x =>
{
    var parts = x.Split(" ");
    return (A: int.Parse(parts[0]), B: int.Parse(parts[1]), W: int.Parse(parts[2]));
}).ToDictionary(
    elem => elem.A, 
    (elem, _) => new List<(int DestinationVertexId, int Weight)> { (DestinationVertexId: elem.B, Weight: elem.W) }, 
    (elem, _, val) => val.Add((DestinationVertexId: elem.B, Weight: elem.W)));

static void PrintDijkstraPathLengths(IDictionary<int, List<(int DestinationVertexId, int Weight)>> V, IEnumerable<int> uniqueVertices)
{
    var dist = new Dictionary<int, double>();
    var prev = new Dictionary<int, int?>();

    foreach (var vertex in uniqueVertices)
    {
        dist[vertex] = double.PositiveInfinity;
        prev[vertex] = null;
    }

    dist[1] = 0;

    var Q = new List<int>(uniqueVertices);
    while (Q.Count > 0)
    {
        var min = Q.OrderBy(x => dist[x]).First();
        var u = min;
        Q.Remove(min);

        if (!V.ContainsKey(u))
            continue;

        foreach (var (DestinationVertexId, Weight) in V[u].Where(x => Q.Contains(x.DestinationVertexId)))
        {
            var alt = dist[u] + Weight;
            if (alt < dist[DestinationVertexId])
            {
                dist[DestinationVertexId] = alt;
                prev[DestinationVertexId] = u;
            }
        }
    }

    foreach (var length in dist.OrderBy(x => x.Key).Select(x => x.Value))
        Console.Write($"{(length == double.PositiveInfinity ? -1 : length)} ");
}

PrintDijkstraPathLengths(visitables, Enumerable.Range(1, int.Parse(lines.First().Split(" ")[0])));