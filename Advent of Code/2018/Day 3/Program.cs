var fabricDefinitions = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt").Select(line =>
{
    var split = line.Split(" @ ");
    var rhs = split[1].Split(": ");
    string[] rhslhs = rhs[0].Split(','), rhsrhs = rhs[1].Split('x');
    int fromLeftEdge = int.Parse(rhslhs[0]), width = int.Parse(rhsrhs[0]), fromTopEdge = int.Parse(rhslhs[1]), height = int.Parse(rhsrhs[1]);
    return new
    {
        Id = split[0][1..],
        Coordinates = Enumerable.Range(fromLeftEdge, width).SelectMany(w => Enumerable.Range(fromTopEdge, height).Select(h => (w, h)))
    };
}).ToArray();

 var dict = new Dictionary<(int X, int Y), int>();
foreach (var fabricDefinition in fabricDefinitions)
{
    foreach (var (X, Y) in fabricDefinition.Coordinates)
    {
        if (dict.ContainsKey((X, Y)))
        {
            dict[(X, Y)]++;
        }
        else
        {
            dict.Add((X, Y), 1);
        }
    }
}

Console.WriteLine(dict.Count(kvp => kvp.Value >= 2)); //117948
Console.WriteLine(fabricDefinitions.First(fd => fd.Coordinates.All(c => dict[c] == 1)).Id); //567