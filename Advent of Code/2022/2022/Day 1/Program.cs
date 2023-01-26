var lines = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt").Skip(1);

var topThree = new int[3];
int maxCalories = 0, currentTotal = 0, arrIndex = 0;
foreach (var line in lines)
{
    if (string.IsNullOrWhiteSpace(line))
    {
        if (currentTotal > maxCalories)
            maxCalories = currentTotal;

        if (arrIndex < 3)
        {
            topThree[arrIndex] = currentTotal;
            arrIndex++;
        }
        else 
        {
            int smallest = topThree[0], index = 0;
            for (var i = 1; i < topThree.Length; i++)
            {
                if (topThree[i] < smallest)
                {
                    smallest = topThree[i];
                    index = i;
                }
            }
            
            if (currentTotal > smallest)
                topThree[index] = currentTotal;
        }

        currentTotal = 0;
        continue;
    }

    currentTotal += int.Parse(line);
}

Console.WriteLine(maxCalories);//72017

Console.WriteLine(topThree.Sum());//212520