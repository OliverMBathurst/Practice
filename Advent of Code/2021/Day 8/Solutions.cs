//Part 1
using System.Text;

var input = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt");

var outputLines = input.Select(x => x.Split(" | ")[1].Split(" "));

Console.WriteLine(outputLines.Sum(arr => arr.Count(value => value.Length == 2 || value.Length == 4 || value.Length == 3 || value.Length == 7)));
//519

//Part 2
var inputOutput = input.Select(x =>
{
    var split = x.Split(" | ");
    return new { Input = split[0].Split(" "), Output = split[1].Split(" ") };
});

var mappings = new Dictionary<string, int>
{
    { "abcefg", 0 },
    { "cf", 1 },
    { "acdeg", 2 },
    { "acdfg", 3 },
    { "bcdf", 4 },
    { "abdfg", 5 },
    { "abdefg", 6 },
    { "acf", 7 },
    { "abcdefg", 8 },
    { "abcdfg", 9 }
};

var numbers = new List<int>();

foreach (var io in inputOutput)
{
    var dict = new Dictionary<char, char>();

    string one = io.Input.First(l => l.Length == 2), four = io.Input.First(l => l.Length == 4), seven = io.Input.First(l => l.Length == 3), eight = io.Input.First(l => l.Length == 7);

    var a = seven.Except(one).First();
    dict.Add(a, 'a');

    var eOrG = eight.Except(four + a).ToArray();

    char e = eOrG.First(c => io.Input.Count(x => x.Contains(c)) == 4), g = eOrG.First(c => io.Input.Count(x => x.Contains(c)) == 7);

    dict.Add(e, 'e');
    dict.Add(g, 'g');

    var bOrD = eight.Except(new string(eOrG) + a + one);
    char d = bOrD.First(c => io.Input.Count(x => x.Contains(c)) == 7), b = bOrD.First(c => io.Input.Count(x => x.Contains(c)) == 6);

    dict.Add(b, 'b');
    dict.Add(d, 'd');

    var twoNumbers = new string(new[] { a, d, e, g });
    var twoShape = io.Input.First(x => x.Except(twoNumbers).Count() == 1);

    var c = twoShape.Except(twoNumbers).First();

    dict.Add(one.First(x => x != c), 'f');
    dict.Add(c, 'c');

    var numbersSequence = io.Output.Select(x => x.Select(c => dict[c]))
        .Select(c => mappings[new string(c.OrderBy(x => x).ToArray())].ToString());

    var numStr = new StringBuilder();
    foreach (var s in numbersSequence)
        numStr.Append(s);

    numbers.Add(int.Parse(numStr.ToString()));
}

Console.WriteLine(numbers.Sum());
//1027483