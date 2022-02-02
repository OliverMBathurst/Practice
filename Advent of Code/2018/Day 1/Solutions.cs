var numbers = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt")
    .Select(int.Parse)
    .ToArray();

//Part 1
Console.WriteLine(numbers.Sum());
//486

//Part 2
static int GetSecond(int[] numbers) {
    var currFreq = 0;
    var uniqueFrequencies = new HashSet<int> { 0 };

    while (true)
    {
        for (var i = 0; i < numbers.Length; i++)
        {
            currFreq += numbers[i];
            if (!uniqueFrequencies.Add(currFreq))
                return currFreq;
        }
    }
}

Console.WriteLine(GetSecond(numbers));
//69285