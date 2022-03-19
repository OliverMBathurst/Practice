static void SetRowsAndColumnsToZero(int[][] array)
{
    var zeroedColumns = new HashSet<int>();

    for (var i = 0; i < array.Length; i++)
    {
        for (var j = 0; j < array[i].Length; j++)
        {
            if (zeroedColumns.Contains(j))
                continue;

            if (array[i][j] == 0)
            {
                for (var k = 0; k < array[i].Length; k++)
                    array[i][k] = 0;

                for (var r = 0; r < array.Length; r++)
                    array[r][j] = 0;

                zeroedColumns.Add(j);

                break;
            }
        }
    }
}

var arr = new int[][] {
    new int[] { 1, 5, 45, 0, 81 },
    new int[] { 6, 7, 2, 82, 8 },
    new int[] { 20, 22, 49, 5, 5 },
    new int[] { 0, 23, 50, 0, 92 }
};

SetRowsAndColumnsToZero(arr);

for (var i = 0; i < arr.Length; i++)
{
    for (var j = 0; j < arr[i].Length; j++)
        Console.Write($"{arr[i][j]}\t");

    Console.WriteLine();
}