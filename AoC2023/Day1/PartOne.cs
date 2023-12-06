using Utils;

namespace Day1
{
    internal class PartOne : PartBase<int>
    {
        public override int Run(string[] input)
        {
            int result = 0;
            foreach (var line in input)
            {
                string calibrationValueAsString = new string(
                    new char[]
                    {
                        line.First(char.IsDigit),
                        line.Last(char.IsDigit)
                    });

                int calibartionValue = int.Parse(calibrationValueAsString);
                result += calibartionValue;
            }

            return result;
        }
    }
}
