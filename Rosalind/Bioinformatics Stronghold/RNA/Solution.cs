var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\rosalind_rna.txt");

static IEnumerable<char> GetRNAString(IEnumerable<char> characters)
{
    foreach (var @char in characters)
    {
        yield return @char == 'T' ? 'U' : @char;
    }
}

Console.WriteLine(new string(GetRNAString(lines.SelectMany(x => x)).ToArray()));