IEnumerable<string> 
    lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt"), 
    stackLines = lines.Take(8), 
    instructionLines = lines.Skip(10);

static List<Stack<char>> PerformMoves(List<Stack<char>> stacks, IEnumerable<string> operations, bool partTwo = false)
{
    foreach (var operation in operations)
    {
        var split = operation.Split(" ");
        int numberToMove = int.Parse(split[1]), sourceIdx = int.Parse(split[3]) - 1, destinationIdx = int.Parse(split[5]) - 1;
        
        if (partTwo)
        {
            var popped = new char[numberToMove];

            for (var i = popped.Length - 1; i >= 0; i--)
                popped[i] = stacks[sourceIdx].Pop();

            for (var j = 0; j < popped.Length; j++)
                stacks[destinationIdx].Push(popped[j]);

            continue;
        }

        for (var i = 0; i < numberToMove; i++)
            stacks[destinationIdx].Push(stacks[sourceIdx].Pop());
    }

    return stacks;
}

static List<Stack<char>> GetStacks(IEnumerable<string> stacks) {

    var dict = new Dictionary<int, List<char>>();
    foreach (var line in stacks)
    {
        var i = 0;
        while (i < line.Length)
        {
            if (line[i] == '[')
            {
                if (dict.ContainsKey(i + 1))
                    dict[i + 1].Add(line[i + 1]);
                else
                    dict.Add(i + 1, new List<char> { line[i + 1] });

                i += 3;
            }

            i++;
        }
    }

    return dict.OrderBy(x => x.Key)
        .Select(kvp => new Stack<char>(kvp.Value.Reverse<char>()))
        .ToList();
}

static void PrintStackTops(IEnumerable<Stack<char>> stacks)
{
    foreach (var stack in stacks)
        Console.Write(stack.Pop());

    Console.WriteLine();
}

//Part 1
PrintStackTops(PerformMoves(GetStacks(stackLines), instructionLines));
//BWNCQRMDB

//Part 2
PrintStackTops(PerformMoves(GetStacks(stackLines), instructionLines, true));
//NHWZCBNBF