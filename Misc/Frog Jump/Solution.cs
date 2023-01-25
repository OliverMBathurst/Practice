var stones = new[] { 0, 1, 3, 5, 6, 8, 12, 17 };
var stonesTwo = new[] { 0, 1, 2, 3, 4, 8, 9, 11 };

Console.WriteLine(CanSolve(stones));
Console.WriteLine(CanSolve(stonesTwo));

static bool CanSolve(int[] stones, int stoneIdx = 0, int k = 0)
{
    if (stones.Length == 0 || stones[0] != 0)
        return true;

    if (stoneIdx == 0)
    {
        if (stones[1] != 1)
            return false;

        return CanSolve(stones, 1, 1);
    }

    for (var possibleJump = k - 1; possibleJump < k + 2; possibleJump++)
    {
        if (possibleJump == 0)
            continue;

        var potentialStoneNumber = stones[stoneIdx] + possibleJump;
        for (var i = stoneIdx; i < stones.Length; i++)
            if (stones[i] == potentialStoneNumber && (i == stones.Length - 1 || CanSolve(stones, i, possibleJump)))
                return true;
    }

    return false;
}