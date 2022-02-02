var integers = new[] { 5, 7, 1, 2, 8, 4, 3 };
var target = 10;

var dict = new Dictionary<int, int>();
foreach (var integer in integers)
{
    if (!dict.ContainsKey(integer))
        dict.Add(integer, 1);
    else
        dict[integer]++;
}

foreach (var integer in integers)
{
    var desiredValue = target - integer;
    if (desiredValue == integer)
    {
        if (dict[integer] > 1)
            Console.WriteLine($"Found sum: {integer} and {integer}");
    } 
    else if (dict.ContainsKey(desiredValue))
        Console.WriteLine($"Found sum: {integer} and {desiredValue}");
}