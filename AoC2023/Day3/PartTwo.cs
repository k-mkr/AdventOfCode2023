using System.Drawing;
using Utils;

internal class PartTwo : PartBase<int>
{
    public override int Run(string[] input)
    {
        List<List<int>> starsWithDigitAdjacents = new List<List<int>>();
        Dictionary<Point, int> pointToNumber = new Dictionary<Point, int>();

        List<Point> currentNumber = new List<Point>();
        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                if (char.IsDigit(input[i][j]))
                {
                    currentNumber.Add(new Point(i, j));
                }
                else
                {
                    if (currentNumber.Count != 0)
                    {
                        int number = int.Parse(new string(
                            currentNumber.Select(p => input[p.X][p.Y]).ToArray()));
                        foreach (var digit in currentNumber)
                        {
                            pointToNumber.Add(digit, number);
                        }

                        currentNumber.Clear();
                    }
                }
            }
        }

        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                if (input[i][j] == '*')
                {
                    Point starPoint = new Point(i, j);
                    starsWithDigitAdjacents.Add(GetAdjacentNumbers(starPoint, input, pointToNumber));
                }
            }
        }

        return starsWithDigitAdjacents.Where(x => x.Count == 2).Sum(x => x.Aggregate(1, (total, next) => total * next));
    }

    private List<int> GetAdjacentNumbers(Point point, string[] grid, Dictionary<Point, int> pointsToNumbers)
    {
        List<int> digitAdjacents = new List<int>();

        Func<char, bool> isSymbol = c => char.IsDigit(c);

        int gridX = grid[0].Length;
        int gridY = grid.Length;

        if (point.X - 1 >= 0) // left
        {
            if (isSymbol(grid[point.X - 1][point.Y]))
                digitAdjacents.Add(pointsToNumbers[new Point(point.X - 1, point.Y)]);
        }
        if (point.X + 1 < gridX) // right
        {
            if (isSymbol(grid[point.X + 1][point.Y]))
                digitAdjacents.Add(pointsToNumbers[new Point(point.X + 1, point.Y)]);
        }
        if (point.Y - 1 >= 0) // up
        {
            if (isSymbol(grid[point.X][point.Y - 1]))
                digitAdjacents.Add(pointsToNumbers[new Point(point.X, point.Y - 1)]);
        }
        if (point.Y + 1 < gridY) // down
        {
            if (isSymbol(grid[point.X][point.Y + 1]))
                digitAdjacents.Add(pointsToNumbers[new Point(point.X, point.Y + 1)]);
        }
        if (point.X - 1 >= 0 && point.Y - 1 >= 0) // lerf-up
        {
            if (isSymbol(grid[point.X - 1][point.Y - 1]))
                digitAdjacents.Add(pointsToNumbers[new Point(point.X - 1, point.Y - 1)]);
        }
        if (point.X + 1 < gridX && point.Y - 1 >= 0) // right-up
        {
            if (isSymbol(grid[point.X + 1][point.Y - 1]))
                digitAdjacents.Add(pointsToNumbers[new Point(point.X + 1, point.Y - 1)]);
        }
        if (point.X - 1 >= 0 && point.Y + 1 < gridY) // right-down
        {
            if (isSymbol(grid[point.X - 1][point.Y + 1]))
                digitAdjacents.Add(pointsToNumbers[new Point(point.X - 1, point.Y + 1)]);
        }
        if (point.X + 1 < gridX && point.Y + 1 < gridY) //lef-down
        {
            if (isSymbol(grid[point.X + 1][point.Y + 1]))
                digitAdjacents.Add(pointsToNumbers[new Point(point.X + 1, point.Y + 1)]);
        }

        return digitAdjacents.Distinct().ToList();
    }
}