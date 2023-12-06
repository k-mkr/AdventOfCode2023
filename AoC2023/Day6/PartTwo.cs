using Utils;

internal class PartTwo : PartBase<long>
{
    public override long Run(string[] input)
    {
        int time = int.Parse(string.Join("",PartOne.GetRaceValues(input[0])));

        long distance = long.Parse(string.Join("", PartOne.GetRaceValues(input[1])));

        return PartOne.CalculatePossibleRecordBeat(time, distance);
    }
}