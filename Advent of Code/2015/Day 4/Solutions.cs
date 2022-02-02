//Part 1

using System.Security.Cryptography;
using System.Text;

static int Compute(string input, int zeros) {
    var prefix = new string(Enumerable.Repeat('0', zeros).ToArray());
    int i = 1, answer = 0;
    while (answer == 0) {
        if (Convert.ToHexString(MD5.Create().ComputeHash(Encoding.Default.GetBytes(input + i))).StartsWith(prefix))
            answer = i;
        i++;
    }
    return answer;
}

Console.WriteLine(Compute("ckczppom", 5));
//117946

//Part 2
Console.WriteLine(Compute("ckczppom", 6));
//3938038