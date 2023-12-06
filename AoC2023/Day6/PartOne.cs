using System.Text.RegularExpressions;
using Utils;

internal class PartOne : PartBase<long>
{
    public override long Run(string[] input)
    {
        int[] time = GetRaceValues(input[0]);

        int[] distance = GetRaceValues(input[1]);

        long result = 1;
        for(int i = 0; i < time.Length; i++)
        {
            result *= CalculatePossibleRecordBeat(time[i], distance[i]);
        }

        return result;
    }

    public static int[] GetRaceValues(string input)
    {
        string pattern = @"\b\d+\b";

        return Regex.Matches(input, pattern).Select(x => int.Parse(x.Value)).ToArray();
    }

    public static long CalculatePossibleRecordBeat(long time, long distance)
    {
        int winsCount = 0;
        for (int i = 0; i < time; i++)
        {
            long endOfRaceDistance = i * (time - i);
            if (endOfRaceDistance > distance)
                winsCount++;
        }

        return winsCount;
    }
}