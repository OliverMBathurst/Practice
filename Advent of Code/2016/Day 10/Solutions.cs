
var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt").Select(l => l.Split(" "));
IEnumerable<string[]> valueAssignments = lines.Where(l => l[0] == "value"), instructions = lines.Where(l => l[0] == "bot");

//Part 1
Console.WriteLine(Solve(17, 61, valueAssignments, instructions, false));
//101

//Part 2
Console.WriteLine(Solve(17, 61, valueAssignments, instructions, true));
//37789

static int Solve(int low, int high, IEnumerable<string[]> valueAssignments, IEnumerable<string[]> instructions, bool partTwo)
{
    Dictionary<int, List<int>> outputBin = new(), inputBin = new(), register = GetRegister(valueAssignments);

    IDictionary<int, List<int>> BinSelector(string identifier)
    {
        return identifier switch
        {
            "bot" => register,
            "input" => inputBin,
            "output" => outputBin
        };
    }

    var parsedInstructions = instructions.Select(x =>
    {
        return new
        {
            BotNumber = int.Parse(x[1]),
            LowBin = BinSelector(x[5]),
            LowBinIdentifier = int.Parse(x[6]),
            HighBin = BinSelector(x[10]),
            HighBinIdentifier = int.Parse(x[^1])
        };
    });

    while (true)
    {
        foreach (var instruction in parsedInstructions)
        {
            if (!register.ContainsKey(instruction.BotNumber))
            {
                register.Add(instruction.BotNumber, new List<int>());
                continue;
            }

            var chips = register[instruction.BotNumber];
            if (chips.Count < 2)
                continue;

            var (Low, High) = (chips.Min(), chips.Max());

            if (!partTwo && Low == low && High == high)
            {
                return instruction.BotNumber;
            }
            else if (partTwo && outputBin.ContainsKeys(0, 1, 2))
            {
                return outputBin[0][0] * outputBin[1][0] * outputBin[2][0];
            }
            else
            {
                AddOrUpdate(instruction.LowBin, instruction.LowBinIdentifier, Low, (list, value) => list.Add(value), (value) => new List<int> { value });
                AddOrUpdate(instruction.HighBin, instruction.HighBinIdentifier, High, (list, value) => list.Add(value), (value) => new List<int> { value });
                register[instruction.BotNumber].Clear();
            }
        }
    }
}

static void AddOrUpdate<K, V, T>(IDictionary<K, V> dictionary, K key, T value, Action<V, T> updateAction, Func<T, V> addFunction)
{
    if (dictionary.ContainsKey(key))
        updateAction(dictionary[key], value);
    else
        dictionary.Add(key, addFunction(value));
}

static Dictionary<int, List<int>> GetRegister(IEnumerable<string[]> valueAssignments)
{
    var register = new Dictionary<int, List<int>>();
    foreach (var valueAssignment in valueAssignments)
    {
        var (botNumber, botValue) = (int.Parse(valueAssignment[^1]), int.Parse(valueAssignment[1]));
        AddOrUpdate(register, botNumber, botValue, (list, element) => list.Add(element), (value) => new List<int> { value });
    }

    return register;
}