//Part 1
using System.Collections.Concurrent;

var visits = new ConcurrentDictionary<(int x, int y), int>();

var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt")
    .Select(line => line.Split(" -> "))
    .Select(side => new { Left = side[0].Split(',').Select(int.Parse).ToArray(), Right = side[1].Split(',').Select(int.Parse).ToArray() })
    .Select(shape => new { x1 = shape.Left[0], y1 = shape.Left[1], x2 = shape.Right[0], y2 = shape.Right[1] });

var horizOrVerts = lines.Where(x => x.x1 == x.x2 || x.y1 == x.y2);
foreach (var anon in horizOrVerts)
    for (var i = anon.y1 == anon.y2 ? Math.Max(anon.x1, anon.x2) : Math.Max(anon.y1, anon.y2); i >= (anon.y1 == anon.y2 ? Math.Min(anon.x1, anon.x2) : Math.Min(anon.y1, anon.y2)); i--)
        visits.AddOrUpdate(anon.y1 == anon.y2 ? (i, anon.y1) : (anon.x2, i), 1, (key, val) => val + 1);

Console.WriteLine(visits.Count(x => x.Value >= 2));
//6005

//Part 2
foreach (var coords in lines.Except(horizOrVerts))
    for (int i = coords.x1 > coords.x2 ? coords.x1 : coords.x2, y = 0; i >= (coords.x1 > coords.x2 ? coords.x2 : coords.x1); i--, y++)
        visits.AddOrUpdate((i, coords.x1 > coords.x2 ? (coords.y1 > coords.y2 ? coords.y1 - y : coords.y1 + y) : (coords.y1 > coords.y2 ? coords.y2 + y : coords.y2 - y)), 1, (key, val) => val + 1);

Console.WriteLine(visits.Count(x => x.Value >= 2));
//23864