//Part 1

var numbers = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt").Select(x => x.Split(" ")).ToArray();

var dict = numbers.GroupBy(x => x[0])
    .ToDictionary(x => x.Key, v => v.Select(x => int.Parse(x[1])).Sum());

Console.WriteLine(dict["forward"] * (dict["down"] - dict["up"]));
//1383564

//Part 2

var projected = numbers.Select((x, index) => new { Key = x[0], Value = int.Parse(x[1]), Index = index });
int aimTotal;
var aims = new List<(int Index, int Aim)>();
foreach (var uod in projected.Where(x => x.Key != "forward")) {
    aims.Add((uod.Index, aimTotal += uod.Key == "down" ? uod.Value : -uod.Value));
}

Console.WriteLine(dict["forward"] * projected.Where(x => x.Key == "forward").Sum(x => x.Value * aims.LastOrDefault(a => a.Index < x.Index).Aim));

//1488311643