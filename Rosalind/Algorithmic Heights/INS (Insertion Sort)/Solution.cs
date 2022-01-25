static int GetInsertionSortSwapsCount(int[] arr)
{
    var swaps = 0;
    var sorted = false;

    while (!sorted)
    {
		sorted = true;
        for (var i = 0; i + 1 < arr.Length; i++)
        {
            if (arr[i] > arr[i + 1])
            {
                var tmp = arr[i + 1];
                arr[i + 1] = arr[i];
                arr[i] = tmp;
                swaps++;
				
				sorted = false;
            }
        }
    }

    return swaps;
}

var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\rosalind_ins.txt").ToArray();
var swaps = GetInsertionSortSwapsCount(lines.Skip(1)
    .SelectMany(x => x.Split(" ").Select(int.Parse).ToArray()).Take(int.Parse(lines[0])).ToArray());

Console.WriteLine(swaps);