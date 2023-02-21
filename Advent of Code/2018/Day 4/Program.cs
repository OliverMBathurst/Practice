var times = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt");

static DateTime GetDateTime(string toParse)
{
    var stripped = toParse[1..].Split(" ");
    string[] rhs = stripped[1].Split(':'), lhs = stripped[0].Split('-');
    return new DateTime(year: int.Parse(lhs[0]), month: int.Parse(lhs[1]), day: int.Parse(lhs[2]), hour: int.Parse(rhs[0]), minute: int.Parse(rhs[1]), second: 0, millisecond: 0);
}

var startingShiftTimes = times
    .Select(gd =>
    {
        var sp = gd.Split("] ");
        return (Time: GetDateTime(sp[0]), Id: sp[1].Split(" ")[1]);
    })
    .OrderBy(x => x.Time)
    .ToArray();

var timeRecords = new Dictionary<DateTime, List<(DateTime Start, DateTime End, string GuardId)>>();

var guardId = string.Empty;
DateTime? start = null;

foreach (var (Time, Id) in startingShiftTimes)
{
    switch (Id[0])
    {
        case '#':
            guardId = Id;
            break;
        case 'a':
            start = Time;
            break;
        case 'u':
            var end = Time;
            if (start != null)
            {
                var day = new DateTime(year: Time.Year, month: Time.Month, day: Time.Day);
                if (timeRecords.ContainsKey(day))
                    timeRecords[day].Add((start.Value, end, guardId));
                else
                    timeRecords.Add(day, new List<(DateTime Start, DateTime End, string GuardId)> { (start.Value, end, guardId) });
            }
            break;
    }
}

var timestamps = timeRecords.Values.SelectMany(x => x).GroupBy(x => x.GuardId).ToArray();
var mostAsleep = timestamps.Select(g => new { Id = g.Key, Value = g.Sum(x => (x.End - x.Start).Minutes) }).MaxBy(x => x.Value).Id;

var hours = new Dictionary<string, Dictionary<(int Hour, int Minute), int>>();
foreach (var grouping in timestamps)
{
    var id = grouping.Key;
    hours.Add(id, new Dictionary<(int Hour, int Minute), int>());
    foreach (var (Start, End, _) in grouping)
    {
        var total = (End - Start).Minutes;
        for (var i = 0; i < total; i++)
        {
            var addedMinutes = Start.AddMinutes(i);
            var hoursMinutes = (addedMinutes.Hour, addedMinutes.Minute);
            if (hours[id].ContainsKey(hoursMinutes))
                hours[id][hoursMinutes]++;
            else
                hours[id].Add(hoursMinutes, 1);
        }
    }
}

Console.WriteLine(hours[mostAsleep].MaxBy(x => x.Value).Key.Minute * int.Parse(mostAsleep[1..])); //19025
var guardWithMaximum = hours.MaxBy(kvp => kvp.Value.Max(values => values.Value));
Console.WriteLine(int.Parse(guardWithMaximum.Key[1..]) * guardWithMaximum.Value.MaxBy(c => c.Value).Key.Minute); //23776
