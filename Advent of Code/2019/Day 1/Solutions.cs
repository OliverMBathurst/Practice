var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt").Select(int.Parse);

//Part 1
Console.WriteLine(lines.Sum(num => Math.Floor(num / (double)3) - 2));

static double SumOfFuel(double fuel)
{
    var requiredFuel = Math.Floor(fuel / 3) - 2;
    if (requiredFuel <= 0)
        return 0D;

    return requiredFuel + SumOfFuel(requiredFuel);
}

//Part 2
Console.WriteLine(lines.Sum(num => SumOfFuel(num)));