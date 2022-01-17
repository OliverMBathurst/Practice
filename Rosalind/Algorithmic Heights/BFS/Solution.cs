static void PrintBFSDistances(
    IDictionary<int, List<int>> vertexInfo, 
    HashSet<int> uniqueVertices)
{
    var distances = new Dictionary<int, int> { { 1, 0 } };
    var queue = new Queue<int>();
    queue.Enqueue(1);

    while (queue.Count > 0)
    {
        var dequeued = queue.Dequeue();
        if (!vertexInfo.ContainsKey(dequeued))
            continue;

        foreach (var vertex in vertexInfo[dequeued])
        {
            if (!distances.ContainsKey(vertex))
            {
                queue.Enqueue(vertex);
                distances[vertex] = distances[dequeued] + 1;
            }
        }
    }

    foreach (var vertex in uniqueVertices.OrderBy(x => x))
        Console.Write($"{distances.GetValueOrDefault(vertex, -1)} ");
}

var edges = File.ReadAllLines(@"C:\Users\Oliver\Desktop\rosalind_bfs.txt")
    .Skip(1)
    .Select(x =>
    {
        var arr = x.Split(" ");
        return (VOne: int.Parse(arr[0]), VTwo: int.Parse(arr[1]));
    });

var vertices = edges.SelectMany(x => new[] { x.VOne, x.VTwo }).ToHashSet();

var connectedVertices = edges.ToDictionary(
    elem => elem.VOne,
    (elem, _) => new List<int> { elem.VTwo },
    (elem, _, val) => val.Add(elem.VTwo));

PrintBFSDistances(connectedVertices, vertices);