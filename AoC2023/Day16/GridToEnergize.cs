using System.Drawing;

internal partial class PartOne
{
    public class GridToEnergize
    {
        private readonly Point _startingPoint;
        private readonly Dir _startingDirection;
        private readonly char[,] _grid;
        private readonly HashSet<Point> _energizedTiles = new HashSet<Point>();
        private readonly HashSet<(Point, Dir)> _visitedTilesWithDirection = new HashSet<(Point, Dir)>();

        public GridToEnergize(Point startingPoint, Dir startingDirection, char[,] grid)
        {
            _startingPoint = startingPoint;
            _startingDirection = startingDirection;
            _grid = grid;
        }

        public long Energize()
        {
            Move(_startingPoint, _startingDirection, _grid);

            Console.WriteLine($"StartingPoint: {_startingPoint}, StartingDirection: {_startingDirection}, EnergizedTiles: {_energizedTiles.Count}");

            return _energizedTiles.Count;
        }

        private void Move(Point p, Dir currentDir, char[,] grid)
        {
            if (!_visitedTilesWithDirection.Add((p, currentDir)))
            {
                return;
            }

            if (!TryGetPointValue(p, grid, out char value))
            {
                return;
            }

            EnergizeTile(p);

            switch (currentDir)
            {
                case Dir.Up:
                    MoveUp(p, value, grid);
                    break;
                case Dir.Down:
                    MoveDown(p, value, grid);
                    break;
                case Dir.Left:
                    MoveLeft(p, value, grid);
                    break;
                case Dir.Right:
                    MoveRight(p, value, grid);
                    break;
            }
        }

        private void EnergizeTile(Point p)
        {
            _energizedTiles.Add(p);
        }

        private void MoveUp(Point p, char value, char[,] grid)
        {
            switch (value)
            {
                case '\\':
                    Move(new Point(p.X - 1, p.Y), Dir.Left, grid);
                    break;
                case '/':
                    Move(new Point(p.X + 1, p.Y), Dir.Right, grid);
                    break;
                case '-':
                    Move(new Point(p.X + 1, p.Y), Dir.Right, grid);
                    Move(new Point(p.X - 1, p.Y), Dir.Left, grid);
                    break;
                default:
                    Move(new Point(p.X, p.Y - 1), Dir.Up, grid);
                    break;
            }
        }

        private void MoveDown(Point p, char value, char[,] grid)
        {
            switch (value)
            {
                case '\\':
                    Move(new Point(p.X + 1, p.Y), Dir.Right, grid);
                    break;
                case '/':
                    Move(new Point(p.X - 1, p.Y), Dir.Left, grid);
                    break;
                case '-':
                    Move(new Point(p.X + 1, p.Y), Dir.Right, grid);
                    Move(new Point(p.X - 1, p.Y), Dir.Left, grid);
                    break;
                default:
                    Move(new Point(p.X, p.Y + 1), Dir.Down, grid);
                    break;
            }
        }

        private void MoveLeft(Point p, char value, char[,] grid)
        {
            switch (value)
            {
                case '\\':
                    Move(new Point(p.X, p.Y - 1), Dir.Up, grid);
                    break;
                case '/':
                    Move(new Point(p.X, p.Y + 1), Dir.Down, grid);
                    break;
                case '|':
                    Move(new Point(p.X, p.Y - 1), Dir.Up, grid);
                    Move(new Point(p.X, p.Y + 1), Dir.Down, grid);
                    break;
                default:
                    Move(new Point(p.X - 1, p.Y), Dir.Left, grid);
                    break;
            }
        }

        private void MoveRight(Point p, char value, char[,] grid)
        {
            switch (value)
            {
                case '\\':
                    Move(new Point(p.X, p.Y + 1), Dir.Down, grid);
                    break;
                case '/':
                    Move(new Point(p.X, p.Y - 1), Dir.Up, grid);
                    break;
                case '|':
                    Move(new Point(p.X, p.Y - 1), Dir.Up, grid);
                    Move(new Point(p.X, p.Y + 1), Dir.Down, grid);
                    break;
                default:
                    Move(new Point(p.X + 1, p.Y), Dir.Right, grid);
                    break;
            }
        }

        private bool TryGetPointValue(Point p, char[,] grid, out char value)
        {
            value = default;

            if (p.X >= 0 && p.X < grid.GetLength(0) && p.Y >= 0 && p.Y < grid.GetLength(1))
            {
                value = grid[p.Y, p.X];
                return true;
            }

            return false;
        }
    }
}