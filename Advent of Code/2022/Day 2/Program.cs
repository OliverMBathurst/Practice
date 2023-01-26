var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt");
var shapeScoreMappings = new Dictionary<char, int>
{
    { 'A', 1 },
    { 'B', 2 },
    { 'C', 3 },
    { 'X', 1 },
    { 'Y', 2 },
    { 'Z', 3 }
};


int GetTotalScore(bool stepTwo)
{
    var scoreR = 0;
    foreach (var line in lines)
    {
        var split = line.Split(" ");
        char L = split[0][0], R = split[1][0];
        int shapeScoreL = shapeScoreMappings[L], shapeScoreR = shapeScoreMappings[R];
        
        if (!stepTwo)
        {
            if (shapeScoreL == shapeScoreR)
            {
                scoreR += 3;
            }
            else
            {
                switch (L)
                {
                    case 'A' when R == 'Y':
                        scoreR += 6;
                        break;
                    case 'B' when R != 'X':
                        scoreR += 6;
                        break;
                    case 'C' when R != 'Y':
                        scoreR += 6;
                        break;
                }
            }

            scoreR += shapeScoreR;
        }
        else
        {
            if (R == 'Y')
            {
                scoreR += 3 + shapeScoreL;
            }
            else
            {
                switch (L)
                {
                    case 'A':
                        scoreR += shapeScoreMappings[R == 'X' ? 'C' : 'B'];
                        break;
                    case 'B':
                        scoreR += shapeScoreMappings[R == 'X' ? 'A' : 'C'];
                        break;
                    case 'C':
                        scoreR += shapeScoreMappings[R == 'X' ? 'B' : 'A'];
                        break;
                }
            }

            if (R == 'Z')
                scoreR += 6;
        }
    }

    return scoreR;
}



Console.WriteLine(GetTotalScore(false)); //11841
Console.WriteLine(GetTotalScore(true)); //13022