var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt");

static (int SumOfCycles, char[,] ResultingGrid) GetSumOfCycles(string[] instructions)
{
    int X = 1, cycles = 0, sumCycles = 0, spriteMiddleX = 1;
    var grid = new char[6, 40];

    int height = grid.GetLength(0), length = grid.GetLength(1);
    for (var i = 0; i < height; i++)
        for (var j = 0; j < length; j++)
            grid[i, j] = '.';

    grid[0, 0] = '#';
    grid[0, 1] = '#';
    grid[0, 2] = '#';

    void IncrementSum() {
        if (cycles == 20 || cycles == 60 || cycles == 100 || cycles == 140 || cycles == 180 || cycles == 220)
            sumCycles += cycles * X;
    }

    void SetSprite() {
        var gridRowLength = grid.GetLength(1);
        var columnNumber = cycles % gridRowLength;
        if (columnNumber == spriteMiddleX || columnNumber == spriteMiddleX + 1 || columnNumber == spriteMiddleX - 1)
            grid[(int)Math.Floor((double)(cycles / gridRowLength)), columnNumber] = '#';
    }
    
    foreach (var instruction in instructions)
    {
        var split = instruction.Split(" ");
        if (split[0] == "noop")
        {
            SetSprite();
            cycles++;
            IncrementSum();
        }
        else
        {
            for (var i = 0; i < 2; i++)
            {
                SetSprite();
                cycles++;
                IncrementSum();
            }

            var addX = int.Parse(split[1]);
            X += addX;
            spriteMiddleX += addX;
        }
    }

    return (sumCycles, grid);
}

var (SumOfCycles, ResultingGrid) = GetSumOfCycles(lines);
Console.WriteLine(SumOfCycles); //15220

for (var y = 0; y < ResultingGrid.GetLength(0); y++)
{
    for (var x = 0; x < ResultingGrid.GetLength(1); x++)
    {
        Console.Write(ResultingGrid[y, x]);
    }

    Console.WriteLine();
}

//RFZEKBFA