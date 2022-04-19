var numbers = string.Join(string.Empty, File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt").SelectMany(l => l))
        .Split(',')
        .Select(int.Parse)
        .ToArray();

//Part 1
Console.WriteLine(PartOne(numbers));
//8536

static long PartOne(int[] input)
{
    int currentPosition = 0, skipSize = 0, maxListLength = 256;
    var listOfNumbers = Enumerable.Range(0, maxListLength).ToArray();

    for (var i = 0; i < input.Length; i++)
    {
        for (int k = currentPosition, j = currentPosition + input[i] - 1; j > k; j--, k++)
        {
            int jModifier = j < maxListLength ? j : j % maxListLength, kModifier = k < maxListLength ? k : k % maxListLength;
            (listOfNumbers[kModifier], listOfNumbers[jModifier]) = (listOfNumbers[jModifier], listOfNumbers[kModifier]);
        }

        var newPosition = currentPosition + input[i] + skipSize;
        currentPosition = newPosition < maxListLength
            ? newPosition
            : newPosition % maxListLength;

        skipSize++;
    }

    return listOfNumbers[0] * listOfNumbers[1];
}