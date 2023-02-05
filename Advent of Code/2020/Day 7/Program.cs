var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt");

var bags = new Dictionary<string, string[]>();
foreach (var line in lines)
{
    var split = line.Split(" ");
    var colour = $"{split[0]} {split[1]}";

    if (split[4] == "no")
    {
        bags.Add(colour, Array.Empty<string>());
    }
    else
    {
        var innerBags = string.Join(" ", split[4..]).Split(", ").SelectMany(s =>
        {
            var split = s.Split(" ");
            return Enumerable.Repeat($"{split[1]} {split[2]}", int.Parse(split[0]));
        }).ToArray();

        bags.Add(colour, innerBags);
    }
}

static bool CanContain(
    IDictionary<string, string[]> bags, 
    string targetColour,
    string initialColour,
    HashSet<string> positiveCache,
    HashSet<string> negativeCache)
{
    var innerBags = bags[initialColour];
    if (innerBags.Contains(targetColour))
        return true;

    if (negativeCache.Contains(initialColour))
        return false;

    foreach (var innerColour in innerBags)
        if (!negativeCache.Contains(innerColour) && (positiveCache.Contains(innerColour) || CanContain(bags, targetColour, innerColour, positiveCache, negativeCache)))
            return true;

    return false;
}

static int GetCount(IDictionary<string, string[]> bags, string initialColour)
{
    var innerBags = bags[initialColour].GroupBy(x => x);
    return innerBags.Sum(g => g.Count() + GetCount(bags, g.Key) * g.Count());
}

var i = 0;
HashSet<string> positiveCache = new(), negativeCache = bags.Where(b => b.Value.Length == 0).Select(kvp => kvp.Key).ToHashSet();
foreach (var bag in bags)
{
    if (CanContain(bags, "shiny gold", bag.Key, positiveCache, negativeCache))
    {
        positiveCache.Add(bag.Key);
        i++;
    }
    else
    {
        negativeCache.Add(bag.Key);
    }
}

Console.WriteLine(i); //213
Console.WriteLine(GetCount(bags, "shiny gold")); //38426