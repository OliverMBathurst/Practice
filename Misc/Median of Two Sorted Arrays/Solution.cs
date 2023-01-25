int[] arrOne = new[] { 1, 2 }, arrTwo = new[] { 3, 4 };

var count = arrOne.Length + arrTwo.Length;
var hasSingleMedian = count % 2 != 0;

int idx = 0, arrOneIdx = 0, arrTwoIdx = 0, singleMedianCeiling = (int)Math.Ceiling(count / (double)2);
var median = 0D;

var iterationCount = hasSingleMedian
    ? singleMedianCeiling
    : (count / 2) + 1;

while (idx + 1 <= iterationCount)
{
    var arrOneChosen = arrTwoIdx >= arrTwo.Length || (arrOneIdx < arrOne.Length && arrOne[arrOneIdx] < arrTwo[arrTwoIdx]);

    if (hasSingleMedian)
    {
        if (idx + 1 == singleMedianCeiling)
        {
            median = arrOneChosen ? arrOne[arrOneIdx] : arrTwo[arrTwoIdx];
            break;
        }
    }
    else
    {
        if (idx + 1 == (count / 2))
        {
            median += arrOneChosen ? arrOne[arrOneIdx] : arrTwo[arrTwoIdx];
        }
        else if (idx + 1 == (count / 2) + 1)
        {
            median = (median + (arrOneChosen ? arrOne[arrOneIdx] : arrTwo[arrTwoIdx])) / 2;
            break;
        }
    }

    if (arrOneChosen)
        arrOneIdx++;
    else
        arrTwoIdx++;

    idx++;
}

Console.WriteLine(median);