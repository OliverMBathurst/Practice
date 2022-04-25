using System.Security.Cryptography;
using System.Text;

//Part 1
Console.WriteLine(GetIndices("qzyelonm", 64).Last());
//15168

static IEnumerable<int> GetIndices(string input, int keyCount)
{
    var indices = new List<int>();
    var triplets = new List<Triplet>();
    var idx = 0;
    using (var md5 = MD5.Create())
    {
        while (indices.Count < keyCount)
        {
            var hash = Convert.ToHexString(md5.ComputeHash(Encoding.Default.GetBytes($"{input}{idx}"))).ToLower();

            var hasFoundTriplet = false;
            for (var i = 0; i + 2 < hash.Length; i++)
            {
                if (hash[i] == hash[i + 1] && hash[i] == hash[i + 2])
                {
                    var additional = 0;
                    for (var j = i + 3; j < hash.Length && hash[j] == hash[i]; j++)
                        additional++;

                    if (additional == 2)
                    {
                        var matchingTriplets = triplets.Where(x => x.Character.Equals(hash[i])).ToList();
                        foreach (var matchingTriplet in matchingTriplets.OrderBy(x => x.Index))
                        {
                            indices.Add(matchingTriplet.Index);
                            triplets.Remove(matchingTriplet);
                        }
                    }

                    if (!hasFoundTriplet)
                    {
                        triplets.Add(new Triplet(hash[i], idx, 1000));
                        hasFoundTriplet = true;
                    }
                }
            }

            foreach (var triplet in triplets)
                triplet.DecrementCounter();

            triplets = triplets.Where(x => x.Counter > 0).ToList();

            idx++;
        }
    }

    return indices.OrderBy(x => x).Take(keyCount);
}

class Triplet
{
    public Triplet(char character, int index, int counter)
    {
        Character = character;
        Index = index;
        Counter = counter;
    }

    public int Index { get; }

    public int Counter { get; private set; }

    public char Character { get; }

    public void DecrementCounter()
    {
        Counter--;
    }
}