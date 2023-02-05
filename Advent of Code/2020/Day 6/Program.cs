var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt");

var groups = new List<List<string>> { 
    new List<string>() 
};

foreach (var line in lines)
{
    if (string.IsNullOrWhiteSpace(line))
        groups.Add(new List<string>());
    else
        groups[^1].Add(line);
}

var anyoneAnsweredYes = groups.Sum(g => g.SelectMany(s => s.ToCharArray()).ToHashSet().Count);
Console.WriteLine(anyoneAnsweredYes); //6703

var allAnsweredYes = groups.Sum(g => g.Aggregate<IEnumerable<char>>((a, b) => a.Intersect(b)).Count());
Console.WriteLine(allAnsweredYes); //3430