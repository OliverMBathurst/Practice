//Part 1
var input = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt")[0].Split(',').Select(int.Parse);

static int FindLowestAmountOfFuel(IEnumerable<int> numbers, bool acc) {
    var distincts = numbers.Distinct().ToDictionary(x => x, v => 0);
    foreach (var key in distincts.Select(k => k.Key))
        distincts[key] = numbers.Select(num => Math.Abs(num - key)).Sum(x => !acc ? x : Enumerable.Range(0, x).Select(x => x + 1).Sum());
    return distincts.Min(x => x.Value);
}

Console.WriteLine(FindLowestAmountOfFuel(input, false));
//335330

//Part 2
Console.WriteLine(FindLowestAmountOfFuel(input, true));
//92439766