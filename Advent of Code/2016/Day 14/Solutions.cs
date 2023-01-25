using System.Security.Cryptography;
using System.Text;

const string input = "qzyelonm";

//Part 1
Console.WriteLine(GetIndex(input, 64, false));
//15168

//Part 2
Console.WriteLine(GetIndex(input, 64, true));
//20864

static int GetIndex(string input, int keyCount, bool partTwo)
{
    var tripletDict = new Dictionary<char, List<Triplet>>();
    int idx = 0, indicesCount = 0;
    using var md5 = MD5.Create();
    while (true)
    {
        var hash = Convert.ToHexString(md5.ComputeHash(Encoding.Default.GetBytes($"{input}{idx}"))).ToLower();
        if (partTwo)
            for (var i = 0; i < 2016; i++)
                hash = Convert.ToHexString(md5.ComputeHash(Encoding.Default.GetBytes(hash))).ToLower();

        var hasFoundTriplet = false;
        for (var i = 0; i + 2 < hash.Length; i++)
        {
            if (hash[i] == hash[i + 1] && hash[i] == hash[i + 2])
            {
                var additional = 0;
                for (var j = i + 3; j < hash.Length && hash[j] == hash[i]; j++)
                    additional++;

                if (additional == 2 && tripletDict.ContainsKey(hash[i]))
                {
                    foreach (var matchingTriplet in tripletDict[hash[i]])
                    {
                        indicesCount++;
                        if (indicesCount == (partTwo ? keyCount + 1 : keyCount))
                            return matchingTriplet.Index;
                    }

                    tripletDict[hash[i]].Clear();
                }

                if (!hasFoundTriplet)
                {
                    if (tripletDict.ContainsKey(hash[i]))
                        tripletDict[hash[i]].Add(new Triplet(idx));
                    else
                        tripletDict.Add(hash[i], new List<Triplet> { new Triplet(idx) });
                    hasFoundTriplet = true;
                }
            }
        }

        foreach (var key in tripletDict.Select(kvp => kvp.Key))
            tripletDict[key] = tripletDict[key].Where(x => x.DecrementCounter()).ToList();

        idx++;
    }
}

class Triplet
{
    public Triplet(int index)
    {
        Index = index;
        Counter = 1000;
    }

    public int Index { get; init; }

    public int Counter { get; private set; }

    public bool DecrementCounter()
    {
        return Counter-- > 0;
    }
}