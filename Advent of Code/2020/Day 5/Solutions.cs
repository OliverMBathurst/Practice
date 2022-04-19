var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt");

var seatIds = lines.Select(i => GetIdentifier(i[..7], 0, 127) * 8 + GetIdentifier(i[7..], 0, 7)).ToHashSet();

//Part 1
Console.WriteLine(seatIds.Max());
//850

//Part 2
Console.WriteLine(Enumerable.Range(seatIds.Min(), seatIds.Max()).Except(seatIds).FirstOrDefault(seatId => seatIds.Contains(seatId + 1) && seatIds.Contains(seatId - 1)));
//599

static int GetIdentifier(string line, int rangeMin, int rangeMax)
{
    var result = 0;
    for (var i = 0; i < line.Length; i++)
    {
        var val = (rangeMax + rangeMin) / (double)2;
        switch (line[i])
        {
            case 'F':
            case 'L':
                rangeMax = (int)val;
                break;
            case 'B':
            case 'R':
                rangeMin = (int)Math.Ceiling(val);
                break;
        }

        if (i == line.Length - 1)
            result = line[i] == 'F' ? rangeMax : rangeMin;
    }

    return result;
}