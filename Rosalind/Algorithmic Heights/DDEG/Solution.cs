static IEnumerable<int> DoubleDegreeArrayDegrees(IEnumerable<(int A, int B)> tuples, int vertices)
{
    var degrees = Enumerable.Range(0, vertices).Select(x => new List<int>(vertices - 1)).ToArray();
    foreach (var (A, B) in tuples)
    {
        degrees[A - 1].Add(B - 1);
        degrees[B - 1].Add(A - 1);
    }
    return degrees.Select(x => x.Sum(x => degrees[x].Count));
}

var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\rosalind_ddeg.txt");

var edges = lines
    .Skip(1)
    .Select(x =>
    {
        var split = x.Split(" ");
        return (A: int.Parse(split[0]), B: int.Parse(split[1]));
    });

foreach (var degree in DoubleDegreeArrayDegrees(edges, int.Parse(lines.First().Split(" ")[0])))
    Console.Write($"{degree} ");