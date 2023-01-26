var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt");

static List<Monkey> GetMonkeys(string[] instructions)
{
    var monkeys = new List<Monkey> { new Monkey() };
    foreach (var line in instructions)
    {
        if (string.IsNullOrWhiteSpace(line))
        {
            monkeys.Add(new Monkey());
        }
        else
        {
            var split = line.Trim().Split(" ").ToArray();
            switch (split[0])
            {
                case "Starting":
                    monkeys[^1].Items.AddRange(split[2..split.Length]
                        .Select(s => {
                            var numStr = s;
                            var sI = s.IndexOf(',');
                            if (sI != -1)
                                numStr = s.Remove(sI, 1);

                            return double.Parse(numStr);
                        }));
                    break;
                case "Operation:":
                    var isOldRightOperand = split[5] == "old";
                    monkeys[^1].Operation = split[4] switch
                    {
                        "+" => (old) => old + (isOldRightOperand ? old : int.Parse(split[5])),
                        "*" => (old) => old * (isOldRightOperand ? old : int.Parse(split[5])),
                        _ => throw new ArgumentException("Invalid operator")
                    };
                    break;
                case "Test:":
                    monkeys[^1].Divisor = int.Parse(split[3]);
                    break;
                case "If":
                    switch (split[1])
                    {
                        case "true:":
                            monkeys[^1].TrueThrow = int.Parse(split[5]);
                            break;
                        case "false:":
                            monkeys[^1].FalseThrow = int.Parse(split[5]);
                            break;
                    }
                    break;
            }
        }
    }
    return monkeys;
}

static void RunRounds(List<Monkey> monkeys, int rounds, bool divideByThree)
{
    var divisors = monkeys.Select(x => x.Divisor);
    var lcm = monkeys.Max(x => x.Divisor);
    while (true)
    {
        var found = true;
        foreach (var div in divisors)
        {
            if (lcm % div != 0)
            {
                found = false;
                break;
            }
        }

        if (found)
            break;

        lcm++;
    }

    for (var i = 0; i < rounds; i++)
    {
        foreach (var monkey in monkeys)
        {
            if (monkey.Items.Count == 0)
                continue;

            foreach (var item in monkey.Items)
            {
                var worryLevel = item;
                monkey.InspectedItem();
                worryLevel = monkey.Operation(worryLevel);
                worryLevel %= lcm;

                if (divideByThree)
                    worryLevel = Math.Floor(worryLevel / 3);

                var monkeyNum = worryLevel % monkey.Divisor == 0
                    ? monkey.TrueThrow
                    : monkey.FalseThrow;

                monkeys[monkeyNum].Items.Add(worryLevel);
            }

            monkey.Items.Clear();
        }
    }
}

var monkeysPartOne = GetMonkeys(lines);
RunRounds(monkeysPartOne, 20, true);
var sumPartOne = monkeysPartOne.Select(x => x.InspectionCount)
    .OrderByDescending(x => x)
    .Take(2)
    .Aggregate((a, b) => a * b);

Console.WriteLine(sumPartOne); //78678

var monkeysPartTwo = GetMonkeys(lines);
RunRounds(monkeysPartTwo, 10000, false);
var sumPartTwo = monkeysPartTwo.Select(x => x.InspectionCount)
    .OrderByDescending(x => x)
    .Take(2)
    .Aggregate((a, b) => a * b);

Console.WriteLine(sumPartTwo); //15333249714

class Monkey {

    public List<double> Items { get; } = new List<double>();

    public Func<double, double>? Operation { get; set; }

    public int Divisor { get; set; }

    public int TrueThrow { get; set; }

    public int FalseThrow { get; set; }

    public double InspectionCount { get; private set; }

    public void InspectedItem() 
    {
        InspectionCount++;
    }
}