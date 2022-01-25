static int GetConnectedComponentsDFS(IDictionary<int, List<int>> G)
{
    var visited = new Dictionary<int, bool>();
    var ccum = 0;

    foreach (var v in G.Keys)
        visited[v] = false;

    foreach (var v in G.Keys)
    {
        if (!visited[v])
        {
            Explore(v, G, visited);
            ccum++;
        }
    }  

    return ccum;
}

static void Explore(int v, IDictionary<int, List<int>> G, IDictionary<int, bool> visited)
{
    visited[v] = true;
    foreach (var u in G[v])
        if (!visited[u])
            Explore(u, G, visited);
}

var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\rosalind_cc.txt");

var edges = lines.Skip(1).Select(x =>
{
    var split = x.Split(' ');
    return new KeyValuePair<int, int>(int.Parse(split[0]), int.Parse(split[1]));
}).ToUndirectedVUDictionary();

Console.WriteLine(Enumerable.Range(1, int.Parse(lines.First().Split(' ')[0])).Count(x => !edges.ContainsKey(x)) + GetConnectedComponentsDFS(edges));