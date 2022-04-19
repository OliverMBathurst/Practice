var instructions = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt").Select(l =>
{
    var split = l.Split(" if ");
    var splitMore = split[0].Split(" ");
    return new Instruction(splitMore[0], splitMore[1], long.Parse(splitMore[2]), split[1]);
});

//Part 1
var register = new Dictionary<string, long>();
var maxValue = ExecuteInstructions(instructions, register);
Console.WriteLine(register.OrderByDescending(x => x.Value).First().Value);
//5752

//Part 2
Console.WriteLine(maxValue);
//6366

static long ExecuteInstructions(IEnumerable<Instruction> instructions, IDictionary<string, long> register)
{
    var maxValue = 0L;
    foreach (var instruction in instructions)
    {
        if (!register.ContainsKey(instruction.Name))
            register.Add(instruction.Name, 0);

        var conditionSplit = instruction.Condition.Split(" ");

        if (!register.ContainsKey(conditionSplit[0]))
            register.Add(conditionSplit[0], 0);

        long rightOperand = long.Parse(conditionSplit[2]), registerValue = register[conditionSplit[0]];
        var conditionSatisfied = conditionSplit[1] switch
        {
            ">=" => registerValue >= rightOperand,
            "<=" => registerValue <= rightOperand,
            ">" => registerValue > rightOperand,
            "<" => registerValue < rightOperand,
            "==" => registerValue == rightOperand,
            "!=" => registerValue != rightOperand,
            _ => throw new Exception()
        };

        if (conditionSatisfied)
        {
            var newRegisterValue = register[instruction.Name];
            newRegisterValue += instruction.Identifier switch
            {
                "inc" => instruction.Value,
                "dec" => -instruction.Value,
                _ => throw new Exception()
            };

            if (newRegisterValue > maxValue)
                maxValue = newRegisterValue;

            register[instruction.Name] = newRegisterValue;
        }
    }

    return maxValue;
}

record struct Instruction(string Name, string Identifier, long Value, string Condition);