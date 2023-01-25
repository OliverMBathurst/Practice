using System.Text;

//Part 1
Console.WriteLine(DiskWipe("10011111011011001", 272));
//10111110010110110

//Part 2
Console.WriteLine(DiskWipe("10011111011011001", 35651584));
//

static string DiskWipe(string input, int diskFillLength)
{
    var sb = new StringBuilder(input);
    while (sb.Length < diskFillLength)
    {
        var tmpLength = sb.Length;
        sb.Append('0');
        for (var i = tmpLength - 1; i >= 0; i--)
        {
            if (sb.Length < diskFillLength)
                sb.Append(sb[i] == '0' ? '1' : '0');
            else
                break;
        }
    }

    var checksumInputDataString = sb.ToString();
    var checksumBuilder = new StringBuilder();
    do
    {
        for (int i = 0, j = 1; i < checksumInputDataString.Length && j < checksumInputDataString.Length; i += 2, j += 2)
            checksumBuilder.Append(checksumInputDataString[i] == checksumInputDataString[j] ? '1' : '0');

        if (checksumBuilder.Length % 2 == 0)
        {
            checksumInputDataString = checksumBuilder.ToString();
            checksumBuilder.Clear();
        }

    } while (checksumBuilder.Length % 2 == 0);

    return checksumBuilder.ToString();
}