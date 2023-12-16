using Utils;

internal class PartOne : PartBase<long>
{
    public override long Run(string[] input)
    {
        string[] steps = input[0].Split(',');

        long result = 0;
        foreach (string step in steps)
        {
            result += Hash(step);
        }

        return result;
    }

    private long Hash(string text)
    {
        long seed = 0;
        long hash = 0;
        foreach (var c in text)
        {
            hash = ((seed + (int)c) * 17) % 256;
            seed = hash;
        }

        return hash;
    }
}