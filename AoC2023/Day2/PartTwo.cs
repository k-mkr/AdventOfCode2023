using Utils;

namespace Day2
{
    internal class PartTwo : PartBase<int>
    {
        public override int Run(string[] input)
        {
            int result = 0;
            foreach (var line in input)
            {
                var segments = line.Split(": ");
                int gameId = int.Parse(segments[0].Split(" ")[1]);

                var maxCubesInGame = PartOne.GetMaxCubesInGame(segments[1]);

                int allColorsMultiplication = 1;
                foreach(var maxCubeInGame in maxCubesInGame)
                {
                    allColorsMultiplication *= maxCubeInGame.Value;
                }

                result += allColorsMultiplication;
            }

            return result;
        }
    }
}
