var rules = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt")
        .Select(x => {
            var split = x.Split(": ");
            string lhs = split[0], rhs = split[1];
            var splitLhs = lhs.Split(" ");
            var range = splitLhs[0].Split("-");
            return new
            {
                Password = rhs,
                Character = splitLhs[1][0],
                Lower = int.Parse(range[0]),
                Upper = int.Parse(range[1])
            };
        });

//Part 1
var validPasswords = rules.Where(x =>
{
    var count = x.Password.Count(p => p == x.Character);
    return count >= x.Lower && count <= x.Upper;
});

Console.WriteLine(validPasswords.Count());
//378

//Part 2
var validPasswordsPartTwo = rules.Where(x =>
{
    char firstC = x.Password[x.Lower - 1], secondC = x.Password[x.Upper - 1];
    return (firstC == x.Character && secondC != x.Character) || (secondC == x.Character && firstC != x.Character);
});

Console.WriteLine(validPasswordsPartTwo.Count());
//280