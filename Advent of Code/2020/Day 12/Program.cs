var instructions = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt");

static int GetRelativePosition(IEnumerable<string> instructions, bool partTwo)
{
    var direction = Direction.East;
    int north = 0, east = 0, waypointNorth = 1, waypointEast = 10;
    foreach (var instruction in instructions)
    {
        var value = int.Parse(instruction[1..]);
        switch (instruction[0])
        {
            case 'N':
                if (partTwo)
                    waypointNorth += value;
                else
                    north += value;
                break;
            case 'S':
                if (partTwo)
                    waypointNorth -= value;
                else
                    north -= value;
                break;
            case 'E':
                if (partTwo)
                    waypointEast += value;
                else
                    east += value;
                break;
            case 'W':
                if (partTwo)
                    waypointEast -= value;
                else
                    east -= value;
                break;
            case 'R' or 'L':
                var turns = value / 90;
                var turnsClockwise = instruction[0] == 'R' ? turns : 4 - turns;
                if (!partTwo)
                {
                    direction = (Direction)(((int)direction + turnsClockwise) % 4);
                }
                else
                {
                    for (var i = 0; i < turnsClockwise; i++)
                    {
                        var tmp = waypointEast;
                        waypointEast = waypointNorth;
                        waypointNorth = -tmp;
                    }
                }
                break;
            case 'F':
                if (partTwo)
                {
                    north += waypointNorth * value;
                    east += waypointEast * value;
                    break;
                }

                switch (direction)
                {
                    case Direction.North:
                        north += value;
                        break;
                    case Direction.South:
                        north -= value;
                        break;
                    case Direction.East:
                        east += value;
                        break;
                    case Direction.West:
                        east -= value;
                        break;
                }
                break;
        }
    }

    return Math.Abs(north) + Math.Abs(east);
}

Console.WriteLine(GetRelativePosition(instructions, false)); //1603
Console.WriteLine(GetRelativePosition(instructions, true)); //52866

internal enum Direction
{
    North,
    East,
    South,
    West
}