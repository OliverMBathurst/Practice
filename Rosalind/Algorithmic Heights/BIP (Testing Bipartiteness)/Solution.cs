using System.Drawing;
using System.Text;

var graphs = File.ReadAllLines(@"C:\Users\Oliver\Desktop\rosalind_bip.txt")
    .Skip(2)
    .Split(x => string.IsNullOrWhiteSpace(x), false)
    .Select(x => x.Skip(1));

var outputStringBuilder = new StringBuilder();
foreach (var graph in graphs)
{
    var edgeDictionary = new Dictionary<string, List<string>>();
    var vertexColours = new Dictionary<string, Color>();

    foreach (var edgeDefinition in graph)
    {
        var split = edgeDefinition.Split(" ");
        if (edgeDictionary.ContainsKey(split[0]))
            edgeDictionary[split[0]].Add(split[1]);
        else
            edgeDictionary.Add(split[0], new List<string> { split[1] });

        if (edgeDictionary.ContainsKey(split[1]))
            edgeDictionary[split[1]].Add(split[0]);
        else
            edgeDictionary.Add(split[1], new List<string> { split[0] });
    }

    var S = edgeDictionary.Keys.First();
    var Q = new Queue<string>();
    Q.Enqueue(S);
    vertexColours.Add(S, Color.Red);

    var isBipartite = true;
    while (Q.Count > 0 && isBipartite)
    {
        var T = Q.Dequeue();
        foreach (var adj in edgeDictionary[T])
        {
            if (!vertexColours.ContainsKey(adj))
            {
                vertexColours.Add(adj, vertexColours[T] == Color.Red ? Color.Black : Color.Red);
                Q.Enqueue(adj);
            }
            else if (vertexColours[adj] == vertexColours[T])
            {
                isBipartite = false;
                break;
            }
        }
    }

    outputStringBuilder.Append($"{(isBipartite ? string.Empty : "-")}1 ");
}

Console.WriteLine(outputStringBuilder.ToString());