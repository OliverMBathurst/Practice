var instructions = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt")
    .Select(x => x.Split(" "))
    .ToArray();

static void ProcessInstructions(string[][] instructions, IDictionary<char, int> register)
{
    var idx = 0;
    while (idx < instructions.Length)
    {
        switch (instructions[idx][0])
        {
            case "cpy":
                register[instructions[idx][^1][0]] = int.TryParse(instructions[idx][1], out var parsed)
                    ? parsed
                    : register[instructions[idx][1][0]];
                idx++;
                break;
            case "inc":
                register[instructions[idx][1][0]]++;
                idx++;
                break;
            case "dec":
                register[instructions[idx][1][0]]--;
                idx++;
                break;
            case "jnz":
                idx += ((int.TryParse(instructions[idx][1], out var jumpParsed) && jumpParsed != 0) || register[instructions[idx][1][0]] != 0) ? int.Parse(instructions[idx][^1]) : 1;
                break;
        }
    }
}

//Part 1
var registerOne = new Dictionary<char, int> { { 'a', 0 }, { 'b', 0 }, { 'c', 0 }, { 'd', 0 } };
ProcessInstructions(instructions, registerOne);
Console.WriteLine(registerOne['a']);
//318020

//Part 2
var registerTwo = new Dictionary<char, int> { { 'a', 0 }, { 'b', 0 }, { 'c', 1 }, { 'd', 0 } };
ProcessInstructions(instructions, registerTwo);
Console.WriteLine(registerTwo['a']);
//9227674