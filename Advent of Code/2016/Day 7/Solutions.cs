var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt");

//Part 1
Console.WriteLine(PartOneCount(lines));
//110

//Part 2
Console.WriteLine(PartTwoCount(lines));
//242

static int PartTwoCount(string[] lines)
{
    var ipCount = 0;
    foreach (var line in lines)
    {
        HashSet<string> abas = new(), babs = new();
        var withinBrackets = false;
        for (var chIdx = 0; chIdx + 2 < line.Length; chIdx++)
        {
            switch (line[chIdx])
            {
                case '[':
                    withinBrackets = true;
                    break;
                case ']':
                    withinBrackets = false;
                    break;
                default:
                    if (line[chIdx] == line[chIdx + 2] && line[chIdx + 1] != line[chIdx])
                    {
                        var str = new string(new[] { line[chIdx], line[chIdx + 1], line[chIdx + 2] });
                        if (!withinBrackets)
                            abas.Add(str);
                        else
                            babs.Add(str);
                    }
                    break;
            }
        }

        if (abas.Any(aba => babs.Any(bab => bab == new string(new[] { aba[1], aba[0], aba[1] }))))
            ipCount++;
    }

    return ipCount;
}

static int PartOneCount(string[] lines)
{
    var ipCount = 0;
    foreach (var line in lines)
    {
        bool withinBrackets = false, abbaFoundOutsideBrackets = false, abbaFoundInsideBrackets = false;
        for (var chIdx = 0; chIdx + 3 < line.Length; chIdx++)
        {
            switch (line[chIdx])
            {
                case '[':
                    withinBrackets = true;
                    break;
                case ']':
                    withinBrackets = false;
                    break;
                default:
                    if (line[chIdx] != line[chIdx + 1] && line[chIdx] == line[chIdx + 3] && line[chIdx + 1] == line[chIdx + 2])
                    {
                        if (!withinBrackets)
                            abbaFoundOutsideBrackets = true;
                        else
                            abbaFoundInsideBrackets = true;
                    }
                    break;
            }
        }

        if (abbaFoundOutsideBrackets && !abbaFoundInsideBrackets)
            ipCount++;
    }

    return ipCount;
}