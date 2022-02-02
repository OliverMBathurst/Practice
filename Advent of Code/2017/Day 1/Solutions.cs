//Part 1
var input = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt")[0];

var countPartOne = 0;
for (var i = 0; i < input.Length; i++)
    if (input[i] == input[i + 1 == input.Length ? 0 : i + 1])
        countPartOne += int.Parse(input[i].ToString());

Console.WriteLine(countPartOne);
//1044

//Part 2
int steps = input.Length / 2, countPartTwo = 0;
for (var i = 0; i < input.Length; i++)
    if (input[i] == input[i + steps >= input.Length ? i + steps - input.Length : i + steps])
        countPartTwo += int.Parse(input[i].ToString());

Console.WriteLine(countPartTwo);
//1054