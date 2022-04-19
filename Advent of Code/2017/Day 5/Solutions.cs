static int GetNumberOfJumps(Func<int, int> transformer)
{
    var instructions = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt")
    .Select(int.Parse)
    .ToArray();

    int instructionOffset = 0, jumps = 0;
    while (instructionOffset >= 0 && instructionOffset < instructions.Length)
    {
        var tmpInstruction = instructions[instructionOffset];
        instructions[instructionOffset] = transformer(instructions[instructionOffset]);

        instructionOffset += tmpInstruction;
        jumps++;
    }

    return jumps;
}

//Part 1
Console.WriteLine(GetNumberOfJumps(i => i + 1));
//354121

//Part 2
Console.WriteLine(GetNumberOfJumps(i => i + (i >= 3 ? -1 : 1)));
//27283023