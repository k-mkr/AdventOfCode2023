using System.Collections.Concurrent;
using Utils;

internal class PartTwo : PartBase<long>
{
    public override long Run(string[] input)
    {
        var recipe = PartOne.GetAlmanac(input);

        ConcurrentBag<long> location = new ConcurrentBag<long>();

        List<Task> tasks = new List<Task>();
        for (int i = 0; i < recipe.Seeds.Length; i += 2)
        {
            int localIdx = i;
            tasks.Add(Task.Run(() =>
            {
                for (int j = 0; j < recipe.Seeds[localIdx + 1]; j++)
                {
                    long seed = recipe.Seeds[localIdx] + j;

                    long seedLocation = PartOne.GetLocation(seed, recipe.Almanac);

                    location.Add(seedLocation);
                }
            }));
        }

        Task.WhenAll(tasks).GetAwaiter().GetResult();

        return location.Min();
    }
}