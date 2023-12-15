using Utils;

internal class PartOne : PartBase<long>
{
    public override long Run(string[] input)
    {
        char[,] platform = CreatePlatform(input);

        platform = TiltPlatform(platform);

        Print(platform);

        long result = CalculateWeight(platform);

        return result;
    }

    public static void Print(char[,] array)
    {
        int rows = array.GetLength(0);
        int cols = array.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(array[i, j]);
            }
            Console.WriteLine();
        }
    }

    public static char[,] CreatePlatform(string[] input)
    {
        char[,] platform = new char[input[0].Length, input.Length];

        for (int i = 0; i < input.Length; i++)
        {
            for (int j = 0; j < input[i].Length; j++)
            {
                platform[i, j] = input[i][j];
            }
        }

        return platform;
    }

    public static char[,] TiltPlatform(char[,] platform)
    {
        int width = platform.GetLength(0);
        int height = platform.GetLength(1);

        char[,] newPlatform = new char[width, height];

        for (int i = 0; i < width; i++)
        {
            char[] row = new char[height];
            for (int j = 0; j < height; j++)
            {
                row[j] = platform[j, i];
            }

            row = TiltColumn(row);

            for (int j = 0; j < row.Length; j++)
            {
                newPlatform[j, i] = row[j];
            }
        }

        return newPlatform;
    }

    private static char[] TiltColumn(char[] column)
    {
        int shapedRockIndex = 0;
        int rocks = 0;
        for (int i = 0; i < column.Length; i++)
        {
            if (column[i] == 'O')
            {
                rocks++;
                column[i] = '.';
                continue;
            }

            if (column[i] == '#')
            {
                for (int j = 0; j < rocks; j++)
                {
                    column[shapedRockIndex + j] = 'O';
                }
                shapedRockIndex = i + 1;
                rocks = 0;
            }
        }

        for (int j = 0; j < rocks; j++)
        {
            column[shapedRockIndex + j] = 'O';
        }

        return column;
    }

    public static long CalculateWeight(char[,] platform)
    {
        long weight = 0;

        int rows = platform.GetLength(0);
        int cols = platform.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                if (platform[i, j] == 'O')
                    weight += cols - i;
            }
        }

        return weight;
    }
}