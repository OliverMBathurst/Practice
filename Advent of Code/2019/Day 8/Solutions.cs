using System.Text;

var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt");

int width = 25, height = 6;

//Part 1
Console.WriteLine(PartOneSolutionOne(lines, width, height));
Console.WriteLine(PartOneSolutionTwo(lines, width, height));
//1716

//Part Two
Console.WriteLine(PartTwoSolutionOne(lines, width, height));
//KFABY

static string PartTwoSolutionOne(string[] lines, int width, int height)
{
    var layers = lines.SelectMany(x => x)
        .Chunk(width * height)
        .Select(x => x.Chunk(width).ToArray())
        .ToArray();

    var strBuilder = new StringBuilder();
    for (var j = 0; j < height; j++)
    {
        for (var i = 0; i < width; i++)
        {
            for (var k = 0; k < layers.Length; k++)
            {
                var charVal = layers[k][j][i];
                if (charVal == '2')
                    continue;

                strBuilder.Append(charVal == '1' ? 'x' : ' ');
                break;
            }
        }
        strBuilder.AppendLine();
    }

    return strBuilder.ToString();
}

static int PartOneSolutionTwo(string[] lines, int width, int height)
{
    return lines.SelectMany(x => x)
        .Chunk(width * height)
        .MinBy(x => x.Count(c => c == '0'))?
        .AggregateOrDefault(
            (first, second) => first * second,
            seq => seq.Count(c => c == '1'),
            seq => seq.Count(c => c == '2')) ?? 0;
}

static int PartOneSolutionOne(string[] lines, int width, int height)
{
    var layers = GetLayers<string, char>(lines, width * height);

    int? minZeroCount = null;
    int minZeroCountLayerIdx = 0;

    for (var i = 0; i < layers.GetLength(0); i++)
    {
        var zeroCount = 0;
        for (var j = 0; j < layers.GetLength(1); j++)
            if (layers[i, j] == '0')
                zeroCount++;

        if (!minZeroCount.HasValue || zeroCount < minZeroCount.Value)
        {
            minZeroCount = zeroCount;
            minZeroCountLayerIdx = i;
        }
    }

    int oneCount = 0, twoCount = 0;
    for (var k = 0; k < layers.GetLength(1); k++)
    {
        var val = layers[minZeroCountLayerIdx, k];
        if (val == '1')
        {
            oneCount++;
        }
        else if (val == '2')
        {
            twoCount++;
        }
    }

    return oneCount * twoCount;
}

static TTarget[,] GetLayers<TEnumerable, TTarget>(IEnumerable<TEnumerable> sequence, int elementsPerLayer)
    where TEnumerable : IEnumerable<TTarget>
{
    if (sequence == null || !sequence.Any())
    {
        return new TTarget[0, 0];
    }

    var layerLength = (int)Math.Ceiling((double)(sequence.Sum(x => x.Count()) / elementsPerLayer));
    var layersArr = new TTarget[layerLength, elementsPerLayer];

    int layerIdx = 0, layerElementIdx = 0;
    foreach (var subsequence in sequence)
    {
        if (subsequence == null || !subsequence.Any())
        {
            continue;
        }

        foreach (var elem in subsequence)
        {
            layersArr[layerIdx, layerElementIdx] = elem;
            if (layerElementIdx == elementsPerLayer - 1)
            {
                layerIdx++;
                layerElementIdx = 0;
            }
            else
            {
                layerElementIdx++;
            }
        }
    }

    return layersArr;
}