static string PrintThreeSumIndexes(IEnumerable<int> collection)
{
    var printValue = "-1";
    var dict = collection.ToDictionary(
        elem => elem,
        (_, idx) => new List<int> { idx + 1 },
        (_, idx, val) => val.Add(idx + 1));

    foreach (var kvpi in dict)
    {
        foreach (var kvpj in dict)
        {
            if (kvpi.Key == kvpj.Key)
                continue;

            int sum = kvpi.Key + kvpj.Key, key = sum < 0 ? Math.Abs(sum) : -sum;
            if (dict.ContainsKey(key))
                return string.Join(" ", new[] { kvpi.Value.First(), kvpj.Value.First(), dict[key].First() }.OrderBy(x => x));
        }
    }

    return printValue;
}

var numbers = File.ReadAllLines(@"C:\Users\Oliver\Desktop\rosalind_3sum.txt")
    .Skip(1)
    .Select(x => x.Split(" ").Select(int.Parse))
    .ToArray();

foreach (var array in numbers)
    Console.WriteLine(PrintThreeSumIndexes(array));