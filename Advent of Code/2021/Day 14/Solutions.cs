var input = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt");

var insertionDictionary = input.Skip(2).TakeWhile(x => !string.IsNullOrWhiteSpace(x))
    .Select(x => {
        var split = x.Split(" -> ");
        return new { Pair = split[0], Character = split[1][0] };
    })
    .ToDictionary(k => k.Pair, v => v.Character);

static long GetOccurrences(string template, int steps, IDictionary<string, char> mappings)
{
    var pairs = new Dictionary<string, long>();
    for (var i = 0; i < template.Length - 1; i++)
    {
        var key = template[i].ToString() + template[i + 1];
        pairs[key] = pairs.ContainsKey(key) ? pairs[key] + 1 : 1;
    }

    var chars = template.GroupBy(x => x, v => template.Count(t => t == v))
        .ToDictionary(k => k.Key, v => v.LongCount());

    for (var i = 0; i < steps; i++)
    {
        var newPairs = new Dictionary<string, long>();
        foreach (var kvp in pairs)
        {
            var newChar = mappings[kvp.Key];

            chars[newChar] = chars.ContainsKey(newChar) ? chars[newChar] + kvp.Value : kvp.Value;

            var left = $"{kvp.Key[0]}{newChar}";
            newPairs[left] = newPairs.ContainsKey(left) ? newPairs[left] + kvp.Value : kvp.Value;

            var right = $"{newChar}{kvp.Key[1]}";
            newPairs[right] = newPairs.ContainsKey(right) ? newPairs[right] + kvp.Value : kvp.Value;
        }

        pairs = newPairs;
    }

    return chars.Max(x => x.Value) - chars.Min(x => x.Value);
}

//Part 1
var partOne = GetOccurrences(input.First(), 10, insertionDictionary);
Console.WriteLine(partOne);
//2068

//Part 2
var partTwo = GetOccurrences(input.First(), 40, insertionDictionary);
Console.WriteLine(partTwo);
//2158894777814