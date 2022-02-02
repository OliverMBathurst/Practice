static IEnumerable<int> Kahns(IDictionary<int, List<int>> edges, Queue<int> S)
{
    var L = new List<int>();
    while (S.Count > 0)
    {
        var n = S.Dequeue();
        L.Add(n);

        if (!edges.ContainsKey(n))
            continue;

        while (edges[n].Count > 0)
        {
            var m = edges[n][0];
            edges[n].RemoveAt(0);

            if (!edges.Any(x => x.Value.Contains(m)))
                S.Enqueue(m);
        }
    }

    return L;
}

var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\rosalind_ts.txt").ToArray();

var outgoingEdges = lines.Skip(1).Select(x =>
{
    var split = x.Split(' ');
    return (u: int.Parse(split[0]), v: int.Parse(split[1]));
}).ToDictionary(
    x => x.u, 
    (uv, _) => new List<int> { uv.v }, 
    (uv, _, val) => val.Add(uv.v));

var S = new Queue<int>();
foreach (var s in Enumerable.Range(1, int.Parse(lines[0].Split(' ')[0])).Except(outgoingEdges.SelectMany(x => x.Value).Distinct()))
    S.Enqueue(s);

foreach (var num in Kahns(outgoingEdges, S))
    Console.Write($"{num} ");