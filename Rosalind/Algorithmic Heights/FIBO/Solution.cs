static long FibNumAtIndex(int index) 
{
    if (index == 0)
        return 0;

    if (index == 1 || index == 2)
        return 1;

    long beforeNMinusOne = 1, beforeNMinusTwo = 1, current = 0;

    for (var idx = 3; idx < index + 1; idx++)
    {
        current = beforeNMinusOne + beforeNMinusTwo;
        beforeNMinusTwo = beforeNMinusOne;
        beforeNMinusOne = current;
    }

    return current;
}

Console.WriteLine(FibNumAtIndex(20));