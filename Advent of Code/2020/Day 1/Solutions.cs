//Part 1
var input = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt")
    .Where(x => !string.IsNullOrWhiteSpace(x))
    .Select(int.Parse)
    .ToArray();

for (var i = 0; i + 1 < input.Length; i++)
    for (var j = i + 1; j < input.Length; j++)
        if (input[i] + input[j] == 2020)
            Console.WriteLine(input[i] * input[j]); //224436

//Part 2
for (var i = 0; i < input.Length - 2; i++)
    for (var j = i + 1; j < input.Length - 1; j++)
        for (var k = j + 1; k < input.Length; k++)
            if (input[i] + input[j] + input[k] == 2020)
                Console.WriteLine(input[i] * input[j] * input[k]); //303394260