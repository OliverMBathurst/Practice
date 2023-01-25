var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\rosalind_dna.txt");

var occurrenceDictionary = new Dictionary<char, int>
{
    { 'A', 0 },
    { 'C', 0 },
    { 'G', 0 },
    { 'T', 0 }
};

foreach (var line in lines)
    foreach (var @char in line)
        occurrenceDictionary[@char]++;

foreach (var kvp in occurrenceDictionary)
    Console.Write($"{kvp.Value} ");