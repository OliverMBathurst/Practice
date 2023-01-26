var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt");

int GetSum(bool partTwo)
{
    var total = 0;
    foreach (var line in lines)
    {
        var split = line.Split(',');
        string[] splitL = split[0].Split('-'), splitR = split[1].Split('-');

        int ll = int.Parse(splitL[0]), lr = int.Parse(splitL[1]);
        int rl = int.Parse(splitR[0]), rr = int.Parse(splitR[1]);

        if (partTwo && ((ll >= rl && ll <= rr) || (lr >= rl && lr <= rr) || (rl >= ll && rl <= lr) || (rr >= ll && rr <= lr)) || (ll >= rl && lr <= rr) || (rl >= ll && rr <= lr))
            total++;
    }

    return total;
}

Console.WriteLine(GetSum(false)); //644
Console.WriteLine(GetSum(true)); //926