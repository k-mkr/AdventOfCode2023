using System.Text.RegularExpressions;
using Utils;

internal class PartOne : PartBase<long>
{
    public override long Run(string[] input)
    {
        var recipe = GetAlmanac(input);

        List<long> location = new List<long>();
        foreach(var seed in recipe.Seeds)
        {
            long seedLocation = GetLocation(seed, recipe.Almanac);

            location.Add(seedLocation);
        }

        return location.Min();
    }

    public static (long[] Seeds, List<long[][]> Almanac) GetAlmanac(string[] input)
    {
        List<long[][]> almanac = new List<long[][]>();

        long[] seeds = GetSeeds(input[0]);
        List<long[]> conversionRecipe = new List<long[]>();
        for (int i = 1; i < input.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(input[i]))
            {
                i++;
                if (conversionRecipe.Count > 0)
                {
                    almanac.Add(conversionRecipe.ToArray());
                    conversionRecipe.Clear();
                }

                continue;
            }
            else
            {
                long[] recipe = input[i].Split(' ').Select(long.Parse).ToArray();
                conversionRecipe.Add(recipe);
            }
        }

        if (conversionRecipe.Count > 0)
        {
            almanac.Add(conversionRecipe.ToArray());
        }

        return (seeds, almanac);
    }

    public static long[] GetSeeds(string seeds)
    {
        string pattern = @"\b\d+\b";

        return Regex.Matches(seeds, pattern).Select(x => long.Parse(x.Value)).ToArray();
    }

    public static long GetLocation(long seed, List<long[][]> recipes)
    {
        long categoryVal = seed;
        foreach (var category in recipes)
        {
            foreach (var categoryLine in category)
            {
                bool isInRange = categoryVal >= categoryLine[1] && categoryVal <= categoryLine[1] + categoryLine[2];
                if (isInRange)
                {
                    categoryVal = (categoryVal - categoryLine[1]) + categoryLine[0];
                    break;
                }
            }
        }

        return categoryVal;
    }
}