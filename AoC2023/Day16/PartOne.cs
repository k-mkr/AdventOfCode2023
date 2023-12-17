using System.Drawing;
using Utils;

internal partial class PartOne : PartBase<long>
{
    public enum Dir
    {
        Up,
        Down,
        Left,
        Right,
    }

    public override long Run(string[] input)
    {
        char[,] grid = new char[input.Length, input[0].Length];

        for (int i = 0; i < input.Length; i++)
        {
            for(int j = 0; j < input[i].Length; j++)
            {
                grid[i,j] = input[i][j];
            }
        }

        Point startingPoint = new Point(0, 0);

        GridToEnergize toEnergize = new GridToEnergize(startingPoint, Dir.Right, grid);

        long result = toEnergize.Energize();

        return result;
    }
}