using System.Drawing;
using Utils;
using static PartOne;

internal class PartTwo : PartBase<long>
{
    public override long Run(string[] input)
    {
        int height = input.Length;
        int width = input[0].Length;

        char[,] grid = new char[height, width];

        List<Point> startingPoints = new List<Point>();
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                grid[i, j] = input[i][j];
                if(i == 0 ||  j == 0 || i == height - 1 || j == width - 1)
                {
                    startingPoints.Add(new Point(i, j));
                }
            }
        }

        List<Task<long>> tasks = new List<Task<long>>();
        foreach(var startingPoint in startingPoints)
        {
            foreach(var startingDirection in (Dir[])Enum.GetValues(typeof(Dir)))
            {
                Point startingPointLocal = startingPoint;
                Dir startingDirectionLocal = startingDirection;
                tasks.Add(Task.Run(() =>
                {
                    GridToEnergize toEnergize = new GridToEnergize(startingPointLocal, startingDirectionLocal, grid);

                    return toEnergize.Energize();
                }));
            }
        }

        long result = Task.WhenAll(tasks).GetAwaiter().GetResult().Max();

        return result;
    }
}