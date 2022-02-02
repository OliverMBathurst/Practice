//Part 1
var input = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt")
    .Select(x => x.Split('-').ToArray());

var dict = input.SelectMany(x => x).Distinct().ToDictionary(x => x, v => new Node());

foreach (var line in input)
{
    dict[line[0]].After.Add(new Node { Value = line[1] });
    dict[line[1]].Before.Add(new Node { Value = line[0] });
}

IEnumerable<string> GetPaths(bool twoCaveMax, string currentString = "start")
{
    var split = currentString.Split(',');
    var strings = new List<string> { currentString };
    var last = split[^1];

    if (last == "end" || (last == "start" && currentString != "start"))
        return strings;

    var entry = dict[last];
    var all = entry.Before.Concat(entry.After);

    var excluded = new List<Node>();
    if (twoCaveMax)
    {
        if (split.Any(s => s.ToLower() == s && split.Count(v => v == s) == 2))
        {
            excluded.AddRange(all.Where(x => x.Value.ToLower() == x.Value && split.Contains(x.Value)));
        }
    }
    else
    {
        excluded.AddRange(all.Where(x => x.Value.ToLower() == x.Value && split.Contains(x.Value)));
    }

    foreach (var ent in all.Except(excluded))
        strings.AddRange(GetPaths(twoCaveMax, currentString + "," + ent.Value).Where(x => x.Split(',').Last() == "end"));

    return strings;
}

var partOne = GetPaths(false);
Console.WriteLine(partOne.Count() - 1);
//5528

//Part 2
var partTwo = GetPaths(true);
Console.WriteLine(partTwo.Count() - 1);
//131228

public class Node
{
    public string Value { get; set; }

    public List<Node> After { get; set; } = new List<Node>();

    public List<Node> Before { get; set; } = new List<Node>();
}