//Part 1
var input = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt").Select(x => x.Chunk(1).Select(x => int.Parse(x)).ToArray()).ToArray();

var map = new HashSet<(int x, int y)>();

for (var i = 1; i < input[0].Length - 1; i++)
    if (input[0][i] < input[0][i - 1] && input[0][i] < input[0][i + 1] && input[0][i] < input[1][i])
        map.Add((i, 0));

for (var j = 1; j < input[^1].Length - 1; j++)
    if (input[^1][j] < input[^1][j - 1] && input[^1][j] < input[^1][j + 1] && input[^1][j] < input[^2][j])
        map.Add((j, input.Length - 1));
    

for (var s = 1; s < input.Length - 1; s++)
    if (input[s][0] < input[s - 1][0] && input[s][0] < input[s + 1][0] && input[s][0] < input[s][1])
        map.Add((0, s));
    

for (var t = 1; t < input.Length - 1; t++)
    if (input[t][input.Length - 1] < input[t - 1][input.Length - 1] && input[t][input.Length - 1] < input[t + 1][input.Length - 1] && input[t][input.Length - 1] < input[t][input.Length - 2])
        map.Add((input.Length - 1, t));  

for (var r = 1; r < input.Length - 1; r++)
    for (var j = 1; j < input[r].Length - 1; j++)
        if (input[r][j] < input[r][j + 1] && input[r][j] < input[r][j - 1] && input[r][j] < input[r + 1][j] && input[r][j] < input[r - 1][j])
            map.Add((j, r));
    

if (input[0][0] < input[1][0] && input[0][0] < input[0][1])
    map.Add((0, 0));

if (input[^1][0] < input[^2][0] && input[^1][0] < input[^1][1])
    map.Add((0, input.Length - 1));

if (input[0][^1] < input[0][^2] && input[0][^1] < input[1][^1])
    map.Add((input[0].Length - 1, 0));

if (input[^1][^1] < input[^1][^2] && input[^1][^1] < input[^2][^1])
    map.Add((input.Length - 1, input.Length - 1));

Console.WriteLine(map.Sum(x => 1 + input[x.y][x.x]));
//500

//Part 2
static int GetBasinPopulation(int[][] structure, (int x, int y) current, HashSet<(int x, int y)> visited) {
    var (x, y) = current;

    visited.Add((x, y));

    var explorationRoutes = new List<(int x, int y)>();

    if (x + 1 < structure[y].Length && structure[y][x + 1] != 9)
        explorationRoutes.Add((x + 1, y));
    if (x - 1 >= 0 && structure[y][x - 1] != 9)
        explorationRoutes.Add((x - 1, y));
    if (y + 1 < structure.Length && structure[y + 1][x] != 9)
        explorationRoutes.Add((x, y + 1));
    if (y - 1 >= 0 && structure[y - 1][x] != 9)
        explorationRoutes.Add((x, y - 1));

    var count = 1;

    foreach (var route in explorationRoutes)
        if (!visited.Contains(route))
            count += GetBasinPopulation(structure, route, visited);
        
    return count;
}

var results = map.Select(x => GetBasinPopulation(input, x, new HashSet<(int x, int y)>())).OrderByDescending(x => x).Take(3).Aggregate((x, y) => x * y);

Console.WriteLine(results);
//970200