var numbers = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt").Select(int.Parse).ToArray();

//Part 1
Console.WriteLine(numbers.Where((num, idx) => idx > 0 && num > numbers[idx - 1]).Count());

//Part 2
Console.WriteLine(numbers.Where((num, idx) => idx > 2 && num > numbers[idx - 3]).Count());