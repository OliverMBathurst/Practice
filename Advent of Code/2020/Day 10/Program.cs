var numbers = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt")
    .Select(int.Parse)
    .OrderBy(x => x)
    .ToList();

numbers.Add(numbers[^1] + 3);

static int PartOne(IEnumerable<int> numbers)
{
    int prev = 0, oneJumps = 0, threeJumps = 0;
    foreach (var t in numbers)
    {
        if (t == prev + 1)
        {
            oneJumps++;
        }
        else if (t == prev + 3)
        {
            threeJumps++;
        }

        prev = t;
    }

    return oneJumps * threeJumps;
}

static long PartTwoNaive(Queue<List<int>> Q, IList<int> orderedList, ref long count)
{
    while (Q.TryDequeue(out var dequeued))
    {
        var last = dequeued[^1];
        var nextNumbers = orderedList
            .Where(num => num <= last + 3 && num > last)
            .ToArray();

        if (nextNumbers.Length == 0 && last == orderedList[^1])
        {
            count++;
        }
        else
        {
            foreach (var nextNumber in nextNumbers)
            {
                Q.Enqueue(new List<int>(dequeued.Concat(new[] { nextNumber })));
            }
        }
    }

    return count;
}

static long PartTwo(IEnumerable<int> orderedNumbers)
{
    var fullList = orderedNumbers.Prepend(0).ToArray();

    var listOfDifferences = new List<int>();
    var curr = 0;
    for (var i = 1; i < fullList.Length; i++)
    {
        var diff = fullList[i] - curr;
        listOfDifferences.Add(diff);
        curr = fullList[i];
    }

    var contiguousMappings = new Dictionary<int, int>
    {
        { 2, 2 },
        { 3, 4 },
        { 4, 7 }
    };

    var sum = 1L;
    var currGroup = 0;
    foreach (var diff in listOfDifferences)
    {
        if (diff == 1)
        {
            currGroup++;
        }
        else
        {
            if (currGroup > 1)
            {
                sum *= contiguousMappings[currGroup];
            }
            currGroup = 0;
        }
    }
    
    return sum;
}

Console.WriteLine(PartOne(numbers)); //1885
Console.WriteLine(PartTwo(numbers)); //2024782584832