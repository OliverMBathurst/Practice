//Part 1

var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt").Select(x => x.ToArray());

var mostCommon = lines
    .Select(x => x.Select((c, idx) => new { Character = c, Index = idx }))
    .SelectMany(x => x).GroupBy(x => x.Index).Select(x => x.ToList())
    .Select(y => y.Count(x => x.Character == '1') > y.Count(y => y.Character == '0') ? '1' : '0')
    .ToArray();

Console.WriteLine(Convert.ToInt32(new string(mostCommon), 2) * Convert.ToInt32(new string(mostCommon.Select(x => x == '1' ? '0' : '1').ToArray()), 2));
//3148794

//Part 2
string GetSolution(IEnumerable<char[]> rows, int index, bool mc) {
    var chars = rows.Select(x => x[index]);
    int onCount = chars.Count(x => x == '1'), offCount = chars.Count(x => x == '0');
    var mostCommon = onCount == offCount ? (mc ? '1' : '0') : (onCount > offCount ? (mc ? '1' : '0') : (mc ? '0' : '1'));
    var constrictedRows = rows.Where(x => x[index] == mostCommon);
    if (constrictedRows.Count() == 1) { return new string(constrictedRows.ElementAt(0)); }
    return GetSolution(constrictedRows, index + 1, mc);
}

Console.WriteLine(Convert.ToInt32(GetSolution(lines, 0, true), 2) * Convert.ToInt32(GetSolution(lines, 0, false), 2));
//2795310