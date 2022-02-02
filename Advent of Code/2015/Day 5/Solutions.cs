//Part 1
var input = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt");

Console.WriteLine(input.Count(line => line.Count(c => "aeiou".Contains(c)) >= 3 && line.Where((x, idx) => idx + 1 < line.Length && x == line[idx + 1]).Any() && !new[] { "ab", "cd", "pq", "xy" }.Any(line.Contains)));
//238

//Part 2
var niceLines = new HashSet<string>();
foreach (var l in input.Where(l => l.Where((c, i) => i + 2 < l.Length && c == l[i + 2]).Any()))
    for (var i = 0; i + 3 < l.Length; i++)
        for (var t = i + 2; t + 1 < l.Length; t++)
            if (l[t] == l[i] && l[t + 1] == l[i + 1])
                niceLines.Add(l);

Console.WriteLine(niceLines.Count);
//69