//Parts 1 & 2

var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt");
var winners = new HashSet<int>();
var grids = lines.Skip(1).Select((x, idx) => new { Value = x, Index = idx }).Where(x => x.Value == string.Empty).Select(y => lines.Where((_, idx) => idx > y.Index + 1 && idx < y.Index + 7).ToArray()).Select(x => x.Select(c => c.Split(" ").Where(x => !string.IsNullOrWhiteSpace(x)).Select(int.Parse).ToArray()).ToArray()).ToArray();

void CheckWin(int gridIdx, int rowIdx, int cellIdx) {
    var lastCalled = grids[gridIdx][rowIdx][cellIdx];
    grids[gridIdx][rowIdx][cellIdx] = -999;

    if (grids[gridIdx][rowIdx].Count(x => x == -999) == 5 || grids[gridIdx].Count(x => x[cellIdx] == -999) == 5)
    {
        winners.Add(gridIdx);
        Console.WriteLine($"Grid {gridIdx} has won with a score of: {grids[gridIdx].SelectMany(x => x).Where(x => x != -999).Sum() * lastCalled}");
    }
}

foreach (var num in lines.TakeWhile(x => x.Trim() != string.Empty).ElementAt(0).Split(',').Select(int.Parse))
    for (var gridIdx = 0; gridIdx < grids.Length; gridIdx++)
        for (var rowIdx = 0; rowIdx < grids[gridIdx].Length; rowIdx++)
            for (var cellIdx = 0; cellIdx < grids[gridIdx][rowIdx].Length; cellIdx++)
                if (grids[gridIdx][rowIdx][cellIdx] == num && !winners.Contains(gridIdx))
                    CheckWin(gridIdx, rowIdx, cellIdx);