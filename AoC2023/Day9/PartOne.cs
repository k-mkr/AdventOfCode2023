using Utils;

internal class PartOne : PartBase<long>
{
    public override long Run(string[] input)
    {
        long result = 0;

        foreach(var line in input)
        {
            int[] values = line.Split().Select(x => int.Parse(x)).ToArray();

            result += Extrapolate(values);
        }

        return result;
    }

    public long Extrapolate(int[] values)
    {
        List<int> differences = new List<int>();
        for (int i = 0; i + 1 < values.Length; i++)
        {
            int difference = values[i+1] - values[i];
            differences.Add(difference);
        }

        if (differences.All(x => x == 0))
            return values.Last();

        return values.Last() + Extrapolate(differences.ToArray());
    }
}