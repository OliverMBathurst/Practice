//Part 1
var numbers = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt")
    .Select(x => x.Split('x').Select(int.Parse).ToArray())
    .Select(x => new { Length = x[0], Width = x[1], Height = x[2] });

Console.WriteLine(numbers.Sum(x => 2* x.Length * x.Width + 2* x.Width * x.Height + 2* x.Height * x.Length + Math.Min(Math.Min(x.Length * x.Width, x.Width * x.Height), x.Height * x.Length)));
//1598415

//Part 2
Console.WriteLine(numbers.Sum(x => Math.Min(2*x.Width + 2*x.Length, Math.Min(2 * x.Width + 2 * x.Height, 2 * x.Length + 2 * x.Height)) + (x.Width*x.Height*x.Length)));
//3812909