static void BinSearch(int[] sortedArray, int[] inputs)
{
    var midIdx = (sortedArray.Length - 1) / 2;
    var mid = sortedArray[midIdx];

    foreach (var input in inputs)
    {
        var value = "-1 ";
        if (mid == input)
            value = $"{midIdx + 1} ";
        else
            for (var i = mid > input ? 0 : midIdx + 1; i < (mid > input ? midIdx : sortedArray.Length); i++)
                if (sortedArray[i] == input)
                    value = $"{i + 1} ";

        Console.Write(value);
    }
}

var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\rosalind_bins.txt").ToArray();
var values = lines.Skip(2).SelectMany(x => x.Split(" ").Select(int.Parse));
int n = int.Parse(lines[0]), m = int.Parse(lines[1]);
BinSearch(values.Take(n).ToArray(), values.Skip(n).Take(m).ToArray());