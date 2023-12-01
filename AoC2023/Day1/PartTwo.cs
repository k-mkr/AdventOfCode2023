using Utils;

namespace Day1
{
    internal class PartTwo : PartBase
    {
        private static string[] lettersSpelledDigits =
        [
            "-",
            "one",
            "two",
            "three",
            "four",
            "five",
            "six",
            "seven",
            "eight",
            "nine"
        ];

        public override int Run(string[] input)
        {
            int result = 0;
            foreach (var line in input)
            {

                int[] firstDigitsIndexes = new int[lettersSpelledDigits.Length];
                int[] lastDigitsIndexes = new int[lettersSpelledDigits.Length];
                for (int i = 0; i < lettersSpelledDigits.Length; i++)
                {
                    int firstSpelledDigitIdx = line.IndexOf(lettersSpelledDigits[i]);
                    int firstDigitIdx = line.IndexOf(i.ToString());
                    if(firstSpelledDigitIdx == -1 && firstDigitIdx == -1)
                    {
                        firstDigitsIndexes[i] = int.MaxValue;
                    }
                    else if (firstSpelledDigitIdx == -1)
                    {
                        firstDigitsIndexes[i] = firstDigitIdx;
                    }
                    else if (firstDigitIdx == -1)
                    {
                        firstDigitsIndexes[i] = firstSpelledDigitIdx;
                    }
                    else
                    {
                        firstDigitsIndexes[i] = Math.Min(firstSpelledDigitIdx, firstDigitIdx);
                    }

                    int lastSpelledDigitIdx = line.LastIndexOf(lettersSpelledDigits[i]);
                    int lastDigitIdx = line.LastIndexOf(i.ToString());
                    lastDigitsIndexes[i] = Math.Max(lastSpelledDigitIdx, lastDigitIdx);
                }

                string calibrationValueAsString =
                    IndexOfMin(firstDigitsIndexes).ToString() +
                    IndexOfMax(lastDigitsIndexes).ToString();

                int calibartionValue = int.Parse(calibrationValueAsString);
                result += calibartionValue;
            }

            return result;
        }

        private static int IndexOfMin(int[] input)
        {
            int min = input[0];
            int minIndex = 0;

            for (int i = 1; i < input.Length; ++i)
            {
                if (input[i] < min)
                {
                    min = input[i];
                    minIndex = i;
                }
            }

            return minIndex;
        }

        private static int IndexOfMax(int[] input)
        {
            int max = input[0];
            int minIndex = 0;

            for (int i = 1; i < input.Length; ++i)
            {
                if (input[i] > max)
                {
                    max = input[i];
                    minIndex = i;
                }
            }

            return minIndex;
        }
    }
}
