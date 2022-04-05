static int GetNumberOfPasswords(int start, int end, bool largerGroupExclusion)
{
    var total = 0;
    for (var i = start; i <= end; i++)
    {
        var str = i.ToString();
        bool hasAdjacent = false, alwaysDecreasing = true;
        var idx = 1;
        while (idx < str.Length)
        {
            if (!hasAdjacent && str[idx] == str[idx - 1])
            {
                int k = idx + 1, count = 2;
                while (k < str.Length && str[k] == str[idx])
                {
                    count++;
                    k++;
                }

                if ((largerGroupExclusion && count == 2) || !largerGroupExclusion)
                    hasAdjacent = true;

                idx = k;
            }
            else if (str[idx] - '0' < str[idx - 1] - '0')
            {
                alwaysDecreasing = false;
                break;
            }
            else
            {
                idx++;
            }
        }

        if (hasAdjacent && alwaysDecreasing)
            total++;
    }

    return total;
}

//Part 1
Console.WriteLine(GetNumberOfPasswords(134564, 585159, false));
//1929

//Part 2
Console.WriteLine(GetNumberOfPasswords(134564, 585159, true));
//1306