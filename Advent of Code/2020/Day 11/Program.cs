var rows = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt");

static IDictionary<(int X, int Y), bool> GetSeats(IReadOnlyList<string> rows)
{
    var seats = new Dictionary<(int X, int Y), bool>();
    for (var rowIdx = 0; rowIdx < rows.Count; rowIdx++)
    {
        for (var columnIdx = 0; columnIdx < rows[rowIdx].Length; columnIdx++)
        {
            if (rows[rowIdx][columnIdx] == 'L')
            {
                seats.Add((columnIdx, rowIdx), false);
            }
        }
    }

    return seats;
}

static IEnumerable<KeyValuePair<(int X, int Y), bool>> GetNearestSeats(IDictionary<(int X, int Y), bool> seats, (int X, int Y) tuple)
{
    var (X, Y) = tuple;
    var defaultKvp = new KeyValuePair<(int X, int Y), bool>((-1, -1), false);
    return new[]
    {
        seats.Where(kvp => kvp.Key.X > X && kvp.Key.Y == Y).MinByOrElse(x => x.Key.X, defaultKvp),
        seats.Where(kvp => kvp.Key.X < X && kvp.Key.Y == Y).MinByOrElse(x => X - x.Key.X, defaultKvp),
        seats.Where(kvp => kvp.Key.X == X && kvp.Key.Y < Y).MinByOrElse(x => Y - x.Key.Y, defaultKvp),
        seats.Where(kvp => kvp.Key.X == X && kvp.Key.Y > Y).MinByOrElse(x => x.Key.Y - Y, defaultKvp),
        seats.Where(kvp => kvp.Key.X > X && kvp.Key.Y > Y && kvp.Key.X - X == kvp.Key.Y - Y).MinByOrElse(kvp => kvp.Key.X - X, defaultKvp),
        seats.Where(kvp => kvp.Key.X < X && kvp.Key.Y < Y && X - kvp.Key.X == Y - kvp.Key.Y).MinByOrElse(kvp => X - kvp.Key.X, defaultKvp),
        seats.Where(kvp => kvp.Key.X > X && kvp.Key.Y < Y && kvp.Key.X - X == Y - kvp.Key.Y).MinByOrElse(kvp => kvp.Key.X - X, defaultKvp),
        seats.Where(kvp => kvp.Key.X < X && kvp.Key.Y > Y && X - kvp.Key.X == kvp.Key.Y - Y).MinByOrElse(kvp => X - kvp.Key.X, defaultKvp) 
    };
}

static bool IsCrowded(IDictionary<(int X, int Y), bool> seats, (int X, int Y) tuple, bool partTwo)
{
    var (X, Y) = tuple;

    if (partTwo)
        return GetNearestSeats(seats, tuple).Count(kvp => kvp.Value) >= 5;

    var mappings = new[]
    {
        (X + 1, Y),
        (X - 1, Y),
        (X, Y - 1),
        (X, Y + 1),
        (X + 1, Y + 1),
        (X - 1, Y - 1),
        (X + 1, Y - 1),
        (X - 1, Y + 1)
    };

    return mappings.Count(mapping => seats.TryGetValue(mapping, out var res) && res) >= 4;
}

static bool IsSparse(IDictionary<(int X, int Y), bool> seats, (int X, int Y) tuple, bool partTwo)
{
    var (X, Y) = tuple;
    return partTwo 
        ? GetNearestSeats(seats, tuple).All(kvp => !kvp.Value) 
        : (!seats.TryGetValue((X + 1, Y), out var right) || !right)
           && (!seats.TryGetValue((X - 1, Y), out var left) || !left)
           && (!seats.TryGetValue((X, Y - 1), out var up) || !up)
           && (!seats.TryGetValue((X, Y + 1), out var down) || !down)
           && (!seats.TryGetValue((X + 1, Y + 1), out var bottomRight) || !bottomRight)
           && (!seats.TryGetValue((X - 1, Y - 1), out var topLeft) || !topLeft)
           && (!seats.TryGetValue((X + 1, Y - 1), out var topRight) || !topRight)
           && (!seats.TryGetValue((X - 1, Y + 1), out var bottomLeft) || !bottomLeft);
}

static int RunRounds(IDictionary<(int X, int Y), bool> seats, bool partTwo)
{
    while (true)
    {
        var changes = new List<(int X, int Y, bool NewValue)>();
        foreach (var kvp in seats.Select(x => x.Key))
        {
            switch (seats[kvp])
            {
                case true when IsCrowded(seats, kvp, partTwo):
                    changes.Add((kvp.X, kvp.Y, false));
                    break;
                case false when IsSparse(seats, kvp, partTwo):
                    changes.Add((kvp.X, kvp.Y, true));
                    break;
            }
        }

        if (changes.Count == 0)
        {
            return seats.Values.Count(x => x);
        }

        foreach (var (X, Y, NewValue) in changes)
        {
            seats[(X, Y)] = NewValue;
        }
    }
}

Console.WriteLine(RunRounds(GetSeats(rows), false)); //2494
Console.WriteLine(RunRounds(GetSeats(rows), true)); //2306