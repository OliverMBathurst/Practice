//Part 1
var numbers = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt").First(x => !string.IsNullOrWhiteSpace(x)).Split(',').Select(int.Parse);

double Sum(int days) {
    var fishIndexes = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    foreach (var num in numbers) fishIndexes[num]++;

    for (var day = 0; day < days; day++)
    {
        var tmp = fishIndexes[0];
        for (var s = 0; s < fishIndexes.Length - 1; s++)
            fishIndexes[s] = fishIndexes[s + 1];

        fishIndexes[8] = tmp;
        fishIndexes[6] += tmp;
    }

    return fishIndexes.Sum();
}

Console.WriteLine(Sum(80));
//386536

//Part 2
Console.WriteLine(Sum(256));
//1732821262171