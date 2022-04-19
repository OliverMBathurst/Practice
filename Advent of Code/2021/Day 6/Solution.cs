//Part 1
var numbers = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt").First(x => !string.IsNullOrWhiteSpace(x)).Split(',').Select(int.Parse);

double Sum(int days) {
    var fishIndices = new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    foreach (var num in numbers) fishIndices[num]++;

    for (var day = 0; day < days; day++)
    {
        var tmp = fishIndices[0];
        for (var s = 0; s < fishIndices.Length - 1; s++)
            fishIndices[s] = fishIndices[s + 1];

        fishIndices[8] = tmp;
        fishIndices[6] += tmp;
    }

    return fishIndices.Sum();
}

Console.WriteLine(Sum(80));
//386536

//Part 2
Console.WriteLine(Sum(256));
//1732821262171