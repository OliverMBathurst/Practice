var characters = File.ReadAllLines(@"C:\Users\Oliver\Desktop\rosalind_revc.txt").SelectMany(x => x);

static IEnumerable<char> GetComplementaryDNAString(IEnumerable<char> sequence)
{
    var complementMappings = new Dictionary<char, char> {
        { 'A', 'T' },
        { 'T', 'A' },
        { 'G', 'C' },
        { 'C', 'G' },
    };

    foreach (var @char in sequence.Reverse())
    {
        yield return complementMappings[@char];
    }
}

Console.WriteLine(new string(GetComplementaryDNAString(characters).ToArray()));