var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt");

int GetSum(bool partTwo)
{
    var sum = 0;
    void AddToTotal(char inter)
    {
        sum += char.ToUpper(inter) - 64;
        if (char.IsUpper(inter))
            sum += 26;
    }

    if (partTwo)
        foreach (var line in lines.Chunk(3))
            AddToTotal(line.Aggregate((total, next) => string.Join(string.Empty, total.Intersect(next))).First());
    else
        foreach (var line in lines)
            AddToTotal(line[..(line.Length / 2)].Intersect(line[(line.Length / 2)..]).First());

    return sum;
}

Console.WriteLine(GetSum(false)); //7908
Console.WriteLine(GetSum(true)); //2838