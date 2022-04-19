var passphrases = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt").Select(x => x.Split(" "));

var validPartOnePassphrases = passphrases.Where(passphraseArr => passphraseArr.Distinct().Count() == passphraseArr.Length);

//Part 1
Console.WriteLine(validPartOnePassphrases.Count());
//383

//Part 2
Console.WriteLine(validPartOnePassphrases.Count(passphraseArr =>
{
    for (var i = 0; i < passphraseArr.Length - 1; i++)
        for (var j = i + 1; j < passphraseArr.Length; j++)
            if (passphraseArr[i].Length == passphraseArr[j].Length && passphraseArr[i].All(ch => passphraseArr[j].Contains(ch)))
                return false;

    return true;
}));
//265