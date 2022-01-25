static bool IsCyclical<K>(IDictionary<K, List<K>> G)
{
    return G.Keys.Any(vertex => HasCycle(G, vertex, new HashSet<K>()));
}

static bool HasCycle<K>(IDictionary<K, List<K>> G, K v, HashSet<K> visited)
{
    if (!G.ContainsKey(v))
        return false;

    if (!visited.Add(v))
        return true;

    if (G[v].Any(v => HasCycle(G, v, visited)))
        return true;

    visited.Remove(v);

    return false;
}

var Gs = File.ReadAllLines(@"C:\Users\Oliver\Desktop\rosalind_dag.txt")
    .Skip(2)
    .Split(x => string.IsNullOrWhiteSpace(x), false)
    .Select(chunk => chunk
        .Skip(1)
        .Select(line =>
        {
            var split = line.Split(' ');
            return (u: int.Parse(split[0]), v: int.Parse(split[1]));
        })
        .ToDictionary(
            uv => uv.u,
            (uv, _) => new List<int> { uv.v },
            (uv, idx, val) => val.Add(uv.v)));

foreach (var G in Gs)
    Console.Write($"{(!IsCyclical(G) ? "1" : "-1")} ");