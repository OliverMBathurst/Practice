//Part 1
var input = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt");

var parsed = input.Select(x => x.Split("\t").SelectMany(x => x.Split(" ")))
    .Select(x => x.Select(int.Parse).ToArray());

Console.WriteLine(parsed.Sum(x => x.Max() - x.Min()));
//51833

//Part 2
var c = 0;
foreach (var line in parsed)
    for (var numIdx = 0; numIdx < line.Length - 1; numIdx++)
        if (line[numIdx] != 0)
            for (var dIdx = numIdx + 1; dIdx < line.Length; dIdx++)
                if (line[dIdx] != 0 && line[numIdx] % line[dIdx] == 0 || line[dIdx] % line[numIdx] == 0)
                    c += line[numIdx] % line[dIdx] == 0 ? line[numIdx] / line[dIdx] : line[dIdx] / line[numIdx];

Console.WriteLine(c);
//288