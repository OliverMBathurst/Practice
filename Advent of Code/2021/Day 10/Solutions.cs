//Part 1
using System.Text;

var input = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt");

var mappings = new Dictionary<char, char> {
    { '<', '>' },
    { '[', ']' },
    { '(', ')' },
    { '{', '}' }
};

static (IEnumerable<string> CorruptedLines, string InvalidChars) GetFirstIncorrectClosing(IEnumerable<string> lines, IDictionary<char, char> mappings)
{
    var invalidLines = new HashSet<string>();
    var sum = new StringBuilder();

    foreach (var l in lines)
    {
        var invalidFound = false;
        var stack = new Stack<char>();

        var i = 0;
        while (!invalidFound && i < l.Length)
        {
            var c = l[i];
            if (mappings.ContainsKey(c))
                stack.Push(mappings[c]);
            else
                if (stack.Pop() != c)
                {
                    sum.Append(c);
                    invalidLines.Add(l);
                    invalidFound = true;
                }

            i++;
        }
    }

    return (invalidLines, sum.ToString());
}

var (CorruptedLines, InvalidChars) = GetFirstIncorrectClosing(input, mappings);

var sum = InvalidChars.Sum(x => {
    return x switch
    {
        ')' => 3,
        ']' => 57,
        '}' => 1197,
        '>' => 25137,
        _ => throw new NotImplementedException()
    };
});

Console.WriteLine(sum);
//290691

//Part 2
static IEnumerable<double> GetIncompleteSum(IEnumerable<string> lines, IDictionary<char, char> mappings)
{
    var listOfScores = new List<double>();
    foreach (var l in lines)
    {
        var stack = new Stack<char>();
        foreach (var c in l)
            if (mappings.ContainsKey(c))
                stack.Push(mappings[c]);
            else
                stack.Pop();

        var score = 0d;
        while (stack.Any())
        {
            score *= 5;
            score += stack.Pop() switch
            {
                ')' => 1,
                ']' => 2,
                '}' => 3,
                '>' => 4,
                _ => throw new NotImplementedException()
            };
        }
        
        listOfScores.Add(score);
    }

    return listOfScores;
}

var incompleteLines = input.Except(CorruptedLines);
var sumPartTwo = GetIncompleteSum(incompleteLines, mappings)
    .OrderBy(x => x)
    .ToArray();

Console.WriteLine(sumPartTwo[sumPartTwo.Length / 2]);
//2768166558