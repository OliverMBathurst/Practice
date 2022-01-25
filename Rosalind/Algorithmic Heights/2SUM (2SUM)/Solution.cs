var arrays = File.ReadAllLines(@"C:\Users\Oliver\Desktop\rosalind_2sum.txt")
    .Skip(1)
    .Select(x => x.Split(" ").Select(int.Parse));

foreach (var array in arrays)
{
    var dict = new Dictionary<int, int>();
    var idx = 0;
    var printValue = "-1";

    foreach (var val in array)
    {
        if (dict.ContainsKey(-val))
        {
            printValue = $"{dict[-val]} {idx + 1}";
            break;
        }

        if (!dict.ContainsKey(val))
            dict.Add(val, idx + 1);

        idx++;
    }

    Console.WriteLine(printValue);
}