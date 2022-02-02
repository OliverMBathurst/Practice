//Part 1
var numbers = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt")
    .SelectMany(x => x.Select(c => c == '(' ? 1 : -1))
    .ToArray();

Console.WriteLine(numbers.Sum());
//138

//Part 2

var sum = 0;
for (var i = 0; i < numbers.Length; i++) {
    sum += numbers[i];
    if (sum == -1) {
        Console.WriteLine(i + 1); //1771
        break;
    }
}