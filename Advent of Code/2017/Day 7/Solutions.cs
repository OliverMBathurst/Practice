var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input2.txt");

//Part 1
var split = lines.Where(t => t.Contains(" -> ")).Select(t => t.Split(" -> "));
Console.WriteLine(split.Select(t => t[0].Split(" (")[0]).Except(split.SelectMany(t => t[1].Split(", "))).First());
//eqgvf

//Part 2
Console.WriteLine();
//


static int PartTwo(string[] input)
{
    return 0;
}