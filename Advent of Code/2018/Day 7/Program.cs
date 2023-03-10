using System.Text;

static (string StepOrder, int Seconds) Solution(int workerCount)
{
    var steps = File.ReadAllLines(@"C:\Users\Oliver\Desktop\input.txt").Select(line => {
        var split = line.Split(" ");
        return new { Step = split[7][0], Dependency = split[1][0] };
    });

    var stepsWithDependencies = steps
        .SelectMany(o => new[] { o.Step, o.Dependency })
        .Distinct()
        .ToDictionary(k => k, _ => new List<char>());

    foreach (var stepDefinition in steps)
    {
        stepsWithDependencies[stepDefinition.Step].Add(stepDefinition.Dependency);
    }

    var seconds = 0;
    var sb = new StringBuilder();
    var workers = Enumerable.Range(0, workerCount).Select(_ => new Worker()).ToArray();
    while (stepsWithDependencies.Count > 0)
    {
        int? min = null;
        for (var i = 0; i < workers.Length; i++)
        {
            if (workers[i].SecondsLeft == 0)
            {
                var key = workers[i].CurrentStep;
                if (!char.IsWhiteSpace(key))
                {
                    stepsWithDependencies.Remove(key);

                    sb.Append(key);

                    foreach (var step in stepsWithDependencies)
                    {
                        step.Value.Remove(key);
                    }

                    workers[i].CurrentStep = ' ';
                }

                var availableSteps = stepsWithDependencies.Where(kvp => kvp.Value.Count == 0 && !workers.Any(w => w.CurrentStep == kvp.Key))
                    .OrderBy(x => x.Key);

                if (availableSteps.Any())
                {
                    var newStep = availableSteps.First();
                    workers[i].CurrentStep = newStep.Key;
                    workers[i].SecondsLeft = newStep.Key - 4;
                }
            }

            if ((min == null || workers[i].SecondsLeft < min.Value) && !char.IsWhiteSpace(workers[i].CurrentStep))
            {
                min = workers[i].SecondsLeft;
            }
        }

        var decrement = min ?? 1;
        for (var i = 0; i < workers.Length; i++)
        {
            var res = workers[i].SecondsLeft - decrement;
            workers[i].SecondsLeft = res < 0 ? 0 : res;
        }

        if (workers.All(w => w.SecondsLeft == 0) && stepsWithDependencies.Count == 0)
        {
            return (sb.ToString(), seconds);
        }

        seconds += decrement;
    }

    return (sb.ToString(), seconds);
}

Console.WriteLine(Solution(1).StepOrder); //DFOQPTELAYRVUMXHKWSGZBCJIN
Console.WriteLine(Solution(5).Seconds); //1036

public struct Worker
{
    public Worker() { }

    public char CurrentStep { get; set; } = ' ';

    public int SecondsLeft { get; set; } = 0;
}