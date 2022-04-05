using System.Text.RegularExpressions;

var passportDefinitions = ParsePassports(File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt"));

var requiredFields = new[] {
    "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"
};

//Part 1
Console.WriteLine(GetNumberOfValidPassports(passportDefinitions, requiredFields));
//204

//Part 2
Console.WriteLine(GetValidatedCount(FilterInvalidPassports(passportDefinitions, requiredFields)));
//179

static int GetValidatedCount(IEnumerable<IEnumerable<KeyValuePair<string, string>>> passportDefinitions)
{
    var total = 0;
    var eyeColours = new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

    foreach (var passportDefinition in passportDefinitions)
    {
        var valid = true;
        foreach (var kvp in passportDefinition)
        {
            if (!(kvp.Key switch
            {
                "byr" or "iyr" or "eyr" => YearValidation(kvp),
                "hgt" => HeightValidation(kvp),
                "hcl" when kvp.Value.Length == 7 && Regex.IsMatch(kvp.Value, "#[a-f0-9]{6}") => true,
                "ecl" when eyeColours.Contains(kvp.Value) => true,
                "pid" when kvp.Value.Length == 9 && Regex.IsMatch(kvp.Value, "[0-9]{9}") => true,
                "cid" => true,
                _ => false
            }))
            {
                valid = false;
                break;
            }
        }

        if (valid)
            total++;
    }

    return total;
}

static bool HeightValidation(KeyValuePair<string, string> kvp)
{
    if ((kvp.Value.Length == 5 || kvp.Value.Length == 4)
        && Regex.IsMatch(kvp.Value, "[0-9]{2,3}(cm|in)")
        && int.TryParse(kvp.Value[..^2], out var parsed))
    {
        var isCm = kvp.Value.EndsWith("cm");
        return parsed >= (isCm ? 150 : 59) && parsed <= (isCm ? 193 : 76);
    }

    return false;
}

static bool YearValidation(KeyValuePair<string, string> kvp)
{
    if (kvp.Value.Length != 4)
        return false;

    int min = 0, max = 0;
    switch (kvp.Key)
    {
        case "byr":
            min = 1920;
            max = 2002;
            break;
        case "iyr":
            min = 2010;
            max = 2020;
            break;
        case "eyr":
            min = 2020;
            max = 2030;
            break;
    }

    var parsed = int.Parse(kvp.Value);
    return parsed >= min && parsed <= max;
}

static IEnumerable<IEnumerable<KeyValuePair<string, string>>> FilterInvalidPassports(
    IEnumerable<IEnumerable<KeyValuePair<string, string>>> passportDefinitions,
    string[] requiredFields)
{
    return passportDefinitions.Where(def => requiredFields.All(rf => def.Any(d => d.Key == rf)));
}

static int GetNumberOfValidPassports(
    IEnumerable<IEnumerable<KeyValuePair<string, string>>> passportDefinitions,
    string[] requiredFields)
{
    return passportDefinitions.Count(def => requiredFields.All(rf => def.Any(p => p.Key == rf)));
}

static IEnumerable<IEnumerable<KeyValuePair<string, string>>> ParsePassports(string[] input)
{
    var chunks = new List<List<KeyValuePair<string, string>>>();

    if (input.Any(x => !string.IsNullOrWhiteSpace(x)))
        chunks.Add(new List<KeyValuePair<string, string>>());
    else
        return chunks;

    foreach (var line in input)
    {
        if (line.Length == 0 && chunks[^1].Any())
        {
            chunks.Add(new List<KeyValuePair<string, string>>());
            continue;
        }

        var kvps = line.Split(" ").Select(x =>
        {
            var split = x.Split(":");
            return new KeyValuePair<string, string>(split[0], split[1]);
        }).ToList();

        chunks[^1].AddRange(kvps);
    }

    return chunks;
}