var rooms = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt")
    .Select(line =>
{
    var split = line.Split('-');
    var checkSplit = split[^1].Split('[');

    return (letters: string.Join("-", split[0..^1]), sum: int.Parse(checkSplit[0]), order: checkSplit[1].Split(']')[0]);
});


static int ValidRoomSum(IEnumerable<(string letters, int sum, string order)> roomDeclarations)
{
    var validRoomSum = 0;
    foreach (var (letters, sum, order) in roomDeclarations)
    {
        var frequencies = new Dictionary<char, int>();
        foreach (var letter in letters)
        {
            if (letter == '-')
                continue;
            if (frequencies.ContainsKey(letter))
                frequencies[letter]++;
            else
                frequencies.Add(letter, 1);
        }

        if (order.Any(c => !frequencies.ContainsKey(c)))
            continue;

        var valid = true;
        var orderIdx = 0;
        foreach (var kvp in frequencies.OrderByDescending(x => x.Value))
        {
            if (orderIdx >= order.Length)
                break;

            if (kvp.Key != order[orderIdx] && kvp.Value != frequencies[order[orderIdx]])
            {
                valid = false;
                break;
            }

            orderIdx++;
        }

        if (valid)
            validRoomSum += sum;
    }

    return validRoomSum;
}

//Part 1
Console.WriteLine(ValidRoomSum(rooms));
//185371

//Part 2
static void PrintDecryptedRoom(IEnumerable<(string letters, int sum, string order)> rooms)
{
    foreach (var (letters, sum, _) in rooms)
    {
        var characters = letters.Select(x =>
        {
            if (x == '-')
                return ' ';
            var newIndex = (x % 32) + sum;
            return (char)((newIndex > 26 ? newIndex % 26 : newIndex) + 64);
        });

        if (characters.SequenceEqual("NORTHPOLE OBJECT STORAGE"))
            Console.WriteLine(sum);
    }
}

PrintDecryptedRoom(rooms);
//984