using System.Diagnostics;

var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt");

//Part 1
Console.WriteLine(string.Join(string.Empty, Enumerable.Range(0, 8).Select(num => GetMostFrequentCharAtIndex(lines, num))));
//liwvqppc

//Part 2
Console.WriteLine(string.Join(string.Empty, Enumerable.Range(0, 8).Select(num => GetMostFrequentCharAtIndex(lines, num, true))));
//caqfbzlh

static char GetMostFrequentCharAtIndex(
    string[] lines,
    int index,
    bool leastCommon = false)
{
    var dict = new Dictionary<char, int>();
    foreach (var line in lines)
    {
        Debug.Assert(line.Length > index);
        var ch = line[index];
        if (dict.ContainsKey(ch))
            dict[ch]++;
        else
            dict.Add(ch, 1);
    }

    var ordered = dict.OrderByDescending(x => x.Value);

    return leastCommon
        ? ordered.Last().Key
        : ordered.First().Key;
}