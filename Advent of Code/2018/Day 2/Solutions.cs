var boxIds = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt").ToArray();

//Part 1
int twoSum = 0, threeSum = 0;
foreach (var boxId in boxIds)
{
    var grouping = boxId.GroupBy(x => x);
    if (grouping.Any(x => x.Count() == 2))
        twoSum++;

    if (grouping.Any(x => x.Count() == 3))
        threeSum++;
}

Console.WriteLine(twoSum * threeSum);
//5952

static void GetDiff(string[] lines) 
{
    for (var i = 0; i < lines.Length - 1; i++)
    {
        for (var j = i + 1; j < lines.Length; j++)
        {
            int diffCount = 0, diffIdx = 0;
            for (var k = 0; k < lines[i].Length; k++)
            {
                if (lines[i][k] != lines[j][k])
                {
                    diffCount++;
                    if (diffCount > 1)
                        break;

                    diffIdx = k;
                }
            }

            if (diffCount == 1)
                for (var c = 0; c < lines[i].Length; c++)
                    if (c != diffIdx)
                        Console.Write(lines[i][c]);
        }
    }
}

GetDiff(boxIds);
//krdmtuqjgwfoevnaboxglzjph