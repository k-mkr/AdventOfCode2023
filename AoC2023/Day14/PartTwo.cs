using System.Diagnostics;
using Utils;
using static PartOne;

internal class PartTwo : PartBase<long>
{
    public enum WorldOrientation
    {
        North,
        South,
    }

    public override long Run(string[] input)
    {
        long cycles = 1000000000;


        char[,] platform = CreatePlatform(input);

        List<char[,]> platforms = new List<char[,]>();

        long result = 0;
        for (int i = 0; i < cycles; i++)
        {
            platform = TiltPlatform(platform);

            platform = Rotate90(platform);
            platform = TiltPlatform(platform);

            platform = Rotate90(platform);
            platform = TiltPlatform(platform);

            platform = Rotate90(platform);
            platform = TiltPlatform(platform);

            platform = Rotate90(platform);

            result = CalculateWeight(platform);

            int index = platforms.FindIndex(x => PlatformsAreEqual(x, platform));
            if (index != -1)
            {
                int patternLength = i - index;
                int platformIndex = (int)((cycles - index) % patternLength);

                result = CalculateWeight(platforms[index + platformIndex - 1]);
                break;
            }
            platforms.Add(platform);
        }

        return result;
    }

    static char[,] Rotate90(char[,] arr)
    {
        int rows = arr.GetLength(0);
        int cols = arr.GetLength(1);

        char[,] rotatedArray = new char[cols, rows];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                rotatedArray[j, rows - 1 - i] = arr[i, j];
            }
        }

        return rotatedArray;
    }

    public static bool PlatformsAreEqual(char[,] array1, char[,] array2)
    {
        if (array1.GetLength(0) != array2.GetLength(0) || array1.GetLength(1) != array2.GetLength(1))
        {
            return false;
        }

        for (int i = 0; i < array1.GetLength(0); i++)
        {
            for (int j = 0; j < array1.GetLength(1); j++)
            {
                if (array1[i, j] != array2[i, j])
                {
                    return false;
                }
            }
        }

        return true;
    }
}