using System.Text.RegularExpressions;
using Utils;

namespace Day2
{
    internal class PartOne : PartBase<int>
    {
        Dictionary<string, int> maxCubesDefinedInGame = new Dictionary<string, int>
        {
            { "red", 12 },
            { "green", 13 },
            { "blue", 14 }
        };

        public override int Run(string[] input)
        {
            int result = 0;
            foreach(var line in input)
            {
                var segments = line.Split(": ");
                int gameId = int.Parse(segments[0].Split(" ")[1]);

                var maxCubesInGame = GetMaxCubesInGame(segments[1]);

                if(maxCubesInGame.All(x => x.Value <= maxCubesDefinedInGame[x.Key]))
                {
                    result += gameId;
                }
            }

            return result;
        }

        public static Dictionary<string, int> GetMaxCubesInGame(string game)
        {
            string pattern = @"(?<number>\d+)\s+(?<color>\w+)";

            Regex regex = new Regex(pattern);

            MatchCollection matches = regex.Matches(game);

            Dictionary<string, int> cubesMaxValue = new Dictionary<string, int>();
            foreach (Match match in matches)
            {
                int number = int.Parse(match.Groups["number"].Value);
                string color = match.Groups["color"].Value;

                if (cubesMaxValue.TryGetValue(color, out int currentValue))
                {
                    if (number > currentValue)
                        cubesMaxValue[color] = number;
                }
                else
                    cubesMaxValue[color] = number;
            }

            return cubesMaxValue;
        }
    }
}
