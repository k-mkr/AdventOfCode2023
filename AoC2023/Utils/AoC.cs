namespace Utils
{
    public static class AoC
    {
        public static int Execute<TPartOne, TPartTwo>(Part executingPart, string inputFile)
            where TPartOne : PartBase, new()
            where TPartTwo : PartBase, new()
        {
            PartBase part = executingPart == Part.One
                ? new TPartOne()
                : new TPartTwo();

            string[] input = File.ReadAllLines(inputFile);

            return part.Run(input);
        }
    }
}
