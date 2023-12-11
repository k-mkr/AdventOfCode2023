﻿using System.Drawing;
using Utils;

internal class PartOne : PartBase<long>
{
    public override long Run(string[] input)
    {
        int width = input[0].Length;
        int height = input.Length;

        long result = 0;

        List<Point> galaxies = new List<Point>();

        for(int i = 0; i < height; i++)
        {
            for(int j = 0; j < width; j++)
            {
                if (input[i][j] == '#')
                    galaxies.Add(new Point(i, j));
            }
        }

        int[] rowsWithoutGalaxies = Enumerable.Range(0, width).Except(galaxies.Select(x => x.X).Distinct()).ToArray();
        int[] columnsWithoutGalaxies = Enumerable.Range(0, height).Except(galaxies.Select(x => x.Y).Distinct()).ToArray();

        for(int i = 0; i < galaxies.Count; i++)
        {
            for (int j = i + 1; j < galaxies.Count; j++)
            {
                int distance = Math.Abs(galaxies[i].X - galaxies[j].X) + Math.Abs(galaxies[i].Y - galaxies[j].Y);


                int emptyRowsBetweenGalaxyPair = galaxies[i].X > galaxies[j].X
                    ? rowsWithoutGalaxies.Count(x => (x - galaxies[j].X) * (galaxies[i].X - x) > 0)
                    : rowsWithoutGalaxies.Count(x => (x - galaxies[i].X) * (galaxies[j].X - x) > 0);

                int emptyColumnsBetweenGalaxyPair = galaxies[i].Y > galaxies[j].Y
                    ? columnsWithoutGalaxies.Count(x => (x - galaxies[j].Y) * (galaxies[i].Y - x) > 0)
                    : columnsWithoutGalaxies.Count(x => (x - galaxies[i].Y) * (galaxies[j].Y - x) > 0);

                result += distance + emptyColumnsBetweenGalaxyPair + emptyRowsBetweenGalaxyPair;
            }
        }

        return result;
    }
}