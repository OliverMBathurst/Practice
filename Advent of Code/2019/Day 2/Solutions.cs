var arr = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt")
    .SelectMany(x => x.Split(',').Select(int.Parse))
    .ToArray();

static int[] SetResults(int[] origin, int noun, int verb)
{
    var @new = new int[origin.Length];
    for (var i = 0; i < origin.Length; i++)
    {
        if (i == 1 || i == 2)
            continue;
        @new[i] = origin[i];
    }

    @new[1] = noun;
    @new[2] = verb;

    for (var i = 0; i < @new.Length - 4; i += 4)
    {
        if (@new[i] == 99)
            break;

        @new[@new[i + 3]] = @new[i] switch
        {
            1 => @new[@new[i + 1]] + @new[@new[i + 2]],
            2 => @new[@new[i + 1]] * @new[@new[i + 2]],
            _ => throw new Exception()
        };
    }

    return @new;
}

//Part 1
Console.WriteLine(SetResults(arr, 12, 2)[0]);
//3716250

//Part 2
int maxSum = SetResults(arr, 99, 99)[0], nounDiff = maxSum - SetResults(arr, 98, 99)[0], verbDiff = maxSum - SetResults(arr, 99, 98)[0];
var multiplier = (int)Math.Floor(19690720 / (double)(nounDiff > verbDiff ? nounDiff : verbDiff));

Console.WriteLine((100 * multiplier) + (int)Math.Floor((19690720 - SetResults(arr, nounDiff > verbDiff ? multiplier : 0, nounDiff < verbDiff ? multiplier : 0)[0]) / (double)(nounDiff > verbDiff ? verbDiff : nounDiff)));
//6472