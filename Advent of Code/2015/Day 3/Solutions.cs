//Part 1
using System.Collections.Concurrent;

var chars = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt")[0].ToArray();

static ConcurrentDictionary<(int x, int y), int> Compute(IEnumerable<char> inputs) {
    var delivered = new ConcurrentDictionary<(int x, int y), int>();
    delivered.TryAdd((0, 0), 1);

    var (X, Y) = (0, 0);
    foreach (var input in inputs)
    {
        X = input == '>' ? X + 1 : input == '<' ? X - 1 : X;
        Y = input == '^' ? Y + 1 : input == 'v' ? Y - 1 : Y;
        delivered.AddOrUpdate((X, Y), 1, (key, oldValue) => oldValue + 1);
    }

    return delivered;
}

Console.WriteLine(Compute(chars).Count(x => x.Value >= 1));
//2592

//Part 2
var locations = Compute(chars.Where((x, i) => i % 2 != 0));

foreach (var kvp in Compute(chars.Where((x, i) => i % 2 == 0)))
    locations.AddOrUpdate(kvp.Key, 1, (key, oldValue) => oldValue + kvp.Value);

Console.WriteLine(locations.Count(x => x.Value >= 1));
//2360