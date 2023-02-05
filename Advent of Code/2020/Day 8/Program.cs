var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt");

static (bool Terminates, int AccumulatorValue) GetAccumulatorValue(string[] lines)
{
    int acc = 0, idx = 0;
    var executionCache = new HashSet<int>();
    while (idx >= 0 && idx < lines.Length)
    {
        if (!executionCache.Add(idx))
        {
            return (false, acc);
        }

        var split = lines[idx].Split(" ");
        if (split[0] == "nop")
        {
            idx++;
        }
        else
        {
            var num = int.Parse(split[1]);
            if (split[0] == "acc")
            {
                idx++;
                acc += num;
            }
            else
            {
                idx += num;
            }
        }
    }

    if (idx == lines.Length)
    {
        return (true, acc);
    }

    return (false, acc);
}

Console.WriteLine(GetAccumulatorValue(lines).AccumulatorValue); //1727

var viableInstructions = lines
    .Select((line, idx) => new { Line = line, Index = idx })
    .Where(iwi => iwi.Line.StartsWith("nop") || iwi.Line.StartsWith("jmp"));

var acc = 0;
foreach (var instruction in viableInstructions)
{
    var lineOld = lines[instruction.Index];
    var oldLineSplit = lineOld.Split(" ");
    oldLineSplit[0] = oldLineSplit[0] == "nop" ? "jmp" : "nop";

    lines[instruction.Index] = string.Join(" ", oldLineSplit);

    var (Terminates, AccumulatorValue) = GetAccumulatorValue(lines);
    if (Terminates)
    {
        acc = AccumulatorValue;
        break;
    }

    lines[instruction.Index] = lineOld;
}

Console.WriteLine(acc); //552