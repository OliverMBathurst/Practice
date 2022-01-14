var numbers = File.ReadAllLines(@"C:\Users\Oliver\Desktop\rosalind_maj.txt")
    .Skip(1)
    .Select(x => x.Split(" ").Select(int.Parse).ToArray())
    .Select(x => x.FirstOrElse(i => x.Count(c => c == i) > x.Length / 2, -1));

foreach (var num in numbers)
    Console.Write($"{num} ");

public static class Extensions {
    public static T FirstOrElse<T>(this IEnumerable<T> collection, Func<T, bool> predicate, T @default)
    {
        var matchingElements = collection.Where(predicate);
        return matchingElements.Any() ? matchingElements.First() : @default;
    }
}