//Part 1

static string GetBearing(string bearing, string direction) {
    switch (bearing)
    {
        case "North":
            return direction == "Right" ? "East" : "West";
        case "South":
            return direction == "Right" ? "West" : "East";
        case "East":
            return direction == "Right" ? "South" : "North";
        case "West":
            return direction == "Right" ? "North" : "South";
        default:
            break;
    }
    return string.Empty;
}

static (int x, int y) GetLocations(bool returnTwo) {
    var (X, Y, B) = (0, 0, "North");

    var visitedLocations = new HashSet<(int x, int y)> { (0, 0) };
    var twice = (0, 0);
    var found = false;

    void Add((int x, int y) tuple) {
        if (returnTwo && !found)
        {
            if (visitedLocations.Contains(tuple))
            {
                twice = tuple;
                found = true;
            }
            else
                visitedLocations.Add(tuple);
        }
    }

    foreach (var entry in File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt")[0].Split(", ").Select(x => new { Direction = x[0] == 'R' ? "Right" : "Left", Number = int.Parse(x[1..]) })) {
        B = GetBearing(B, entry.Direction);

        if (returnTwo) {
            if (B == "West" || B == "East")
                for (var i = 1; i <= entry.Number; i++)
                    Add((B == "East" ? X + i : X - i, Y));
            else
                for (var i = 1; i <= entry.Number; i++)
                    Add((X, B == "North" ? Y + i : Y - i));
        }
            

        if (returnTwo && found)
            return twice;

        if (B == "West" || B == "East")
            X += B == "East" ? entry.Number : -entry.Number;
        else
            Y += B == "North" ? entry.Number : -entry.Number;
    }

    return (X, Y);
}

var (X, Y) = GetLocations(false);
Console.WriteLine(Math.Abs(X) + Math.Abs(Y));
//298

//Part 2
var (X2, Y2) = GetLocations(true);
Console.WriteLine(Math.Abs(X2) + Math.Abs(Y2));
//158