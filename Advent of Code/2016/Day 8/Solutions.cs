var dict = ProcessInstructions(50, 6, File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt"));

//Part 1
Console.WriteLine(dict.Sum(x => x.Value.Count));
//106

//Part 2
PrintGrid(50, 6, dict);
//CFLELOYFCS

static void PrintGrid(int gridXLength, int gridYLength, IDictionary<int, HashSet<int>> grid)
{
    for (var i = 0; i < gridYLength; i++)
    {
        for (var j = 0; j < gridXLength; j++)
        {
            Console.Write($"{(grid[i].Contains(j) ? '#' : '.')} ");
        }
        Console.WriteLine();
    }
}

static IDictionary<int, HashSet<int>> ProcessInstructions(int gridXLength, int gridYLength, string[] instructions)
{
    var dict = Enumerable.Range(0, gridYLength).ToDictionary(k => k, v => new HashSet<int>());
    foreach (var instruction in instructions)
    {
        var splitInstruction = instruction.Split(" ");
        if (splitInstruction[0].Equals("rect"))
        {
            var splitMatrix = splitInstruction[1].Split("x");
            int width = int.Parse(splitMatrix[0]), height = int.Parse(splitMatrix[^1]);
            for (var j = 0; j < height; j++)
                for (var i = 0; i < width; i++)
                    dict[j].Add(i);
        }
        else
        {
            int xOrYValue = int.Parse(splitInstruction[^3].Split("=")[^1].ToString()), shift = int.Parse(splitInstruction[^1]), iterationValue = gridXLength;
            var xIsConstant = false;

            if (splitInstruction[1].Equals("column"))
            {
                iterationValue = gridYLength;
                xIsConstant = true;
            }

            var indices = new List<int>(iterationValue);
            for (var i = 0; i < iterationValue; i++)
            {
                int y = xIsConstant ? i : xOrYValue, x = xIsConstant ? xOrYValue : i;
                if (dict[y].Contains(x))
                {
                    var proposedNewIndex = i + shift;
                    indices.Add(proposedNewIndex < iterationValue ? proposedNewIndex : proposedNewIndex % iterationValue);
                    dict[y].Remove(x);
                }
            }

            for (var i = 0; i < indices.Count; i++)
                dict[xIsConstant ? indices[i] : xOrYValue].Add(xIsConstant ? xOrYValue : indices[i]);
        }
    }

    return dict;
}