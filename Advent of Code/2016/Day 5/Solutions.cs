using System.Security.Cryptography;
using System.Text;

//Part 1
PrintPassword("wtnhxymk", false);
//2414bc77

//Part 2
PrintPassword("wtnhxymk", true);
//437e60fc

static void PrintPassword(string input, bool partTwo)
{
    var passwordCharArray = new char?[8];

    using var md5 = MD5.Create();
    for (int i = 0, set = 0; set < 8; i++)
    {
        var converted = Convert.ToHexString(md5.ComputeHash(Encoding.Default.GetBytes($"{input}{i}")));
        for (var j = 0; j < 6; j++)
        {
            if (j == 5)
            {
                int? fallbackIndex = !partTwo ? set : null;
                var index = partTwo && int.TryParse(converted[5].ToString(), out var output) && output < 8
                    ? output
                    : fallbackIndex;

                if (index.HasValue && passwordCharArray[index.Value] == null)
                {
                    passwordCharArray[index.Value] = char.ToLower(!partTwo ? converted[5] : converted[6]);
                    set++;

                    Console.Clear();
                    for (var k = 0; k < passwordCharArray.Length; k++)
                        Console.Write(passwordCharArray[k] == null ? '_' : passwordCharArray[k]);
                }
            }
            else if (converted[j] != '0')
            {
                break;
            }
        }
    }
}