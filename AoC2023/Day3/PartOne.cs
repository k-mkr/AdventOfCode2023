using System.Drawing;
using Utils;

internal class PartOne : PartBase
{
    public override int Run(string[] input)
    {
        int result = 0;

        List<Point> currentNumber = new List<Point>();
        for(int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                if (char.IsDigit(input[i][j]))
                {
                    currentNumber.Add(new Point(i, j));
                }
                else
                {
                    if(currentNumber.Count != 0)
                    {
                        bool hasAdjacentSymbols = TryGetIfAdjacentToSymbol(currentNumber, input, out int output);
                        if(hasAdjacentSymbols)
                        {
                            result += output;
                        }

                        currentNumber.Clear();
                    }
                }
            }
        }

        return result;
    }

    private bool TryGetIfAdjacentToSymbol(List<Point> number, string[] grid, out int output)
    {
        output = 0;
        foreach(var digit in number)
        {
            if(IsAdjacentTo(digit, grid, c => !char.IsDigit(c) && c != '.'))
            {
                output = int.Parse(new string(
                    number.Select(p => grid[p.X][p.Y]).ToArray()));
                return true;
            }
        }

        return false;
    }

    private bool IsAdjacentTo(Point point, string[] grid, Func<char, bool> isSymbol)
    {
        int gridX = grid[0].Length;
        int gridY = grid.Length;

        if(point.X - 1 >= 0) // left
        {
            if (isSymbol(grid[point.X - 1][point.Y]))
                return true;
        }
        if(point.X + 1 < gridX) // right
        {
            if (isSymbol(grid[point.X + 1][point.Y]))
                return true;
        }
        if(point.Y - 1 >= 0) // up
        {
            if (isSymbol(grid[point.X][point.Y - 1]))
                return true;
        }
        if (point.Y + 1 < gridY) // down
        {
            if (isSymbol(grid[point.X][point.Y + 1]))
                return true;
        }
        if(point.X - 1 >= 0 && point.Y - 1 >= 0) // lerf-up
        {
            if (isSymbol(grid[point.X - 1][point.Y - 1]))
                return true;
        }
        if (point.X + 1 < gridX && point.Y - 1 >= 0) // right-up
        {
            if (isSymbol(grid[point.X + 1][point.Y - 1]))
                return true;
        }
        if (point.X - 1 >= 0 && point.Y + 1 < gridY) // right-down
        {
            if (isSymbol(grid[point.X - 1][point.Y + 1]))
                return true;
        }
        if (point.X + 1 < gridX && point.Y + 1 < gridY) //lef-down
        {
            if (isSymbol(grid[point.X + 1][point.Y + 1]))
                return true;
        }

        return false;
    }
}