static IEnumerable<int> DegArray(IEnumerable<(int a, int b)> edges)
{
    var dict = new Dictionary<int, int>();

    foreach (var (a, b) in edges)
    {
        if (dict.ContainsKey(a))
            dict[a]++;
        else
            dict.Add(a, 1);

        if (dict.ContainsKey(b))
            dict[b]++;
        else
            dict.Add(b, 1);
    }

    return dict.OrderBy(x => x.Key)
        .Select(x => x.Value);
}

var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\rosalind_deg.txt")
    .Skip(1)
    .Select(x => {
        var split = x.Split(" ");
        return (a: int.Parse(split[0]), b: int.Parse(split[1]));
    });

foreach (var deg in DegArray(lines))
    Console.Write($"{deg} ");