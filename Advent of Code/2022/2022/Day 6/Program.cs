var str = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt")[0];

static void GetMarker(string str, int messageLength) 
{
    var slidingWindow = str[0..messageLength].ToCharArray();

    if (slidingWindow.ToHashSet().Count == messageLength)
        Console.WriteLine(messageLength + 1);

    for (var i = 4; i < str.Length; i++)
    {
        for (var si = 1; si < slidingWindow.Length; si++)
            slidingWindow[si - 1] = slidingWindow[si];

        slidingWindow[^1] = str[i];

        if (slidingWindow.ToHashSet().Count == messageLength)
        {
            Console.WriteLine(i + 1);
            break;
        }
    }
}


//Part 1
GetMarker(str, 4);
//1093

//Part 2
GetMarker(str, 14);
//3534