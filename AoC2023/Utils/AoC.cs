namespace Utils
{
    public static class AoC
    {
        public static TResult Execute<TPartOne, TPartTwo, TResult>(Part executingPart, string inputFile)
            where TPartOne : PartBase<TResult>, new()
            where TPartTwo : PartBase<TResult>, new()
        {
            PartBase<TResult> part = executingPart == Part.One
                ? new TPartOne()
                : new TPartTwo();

            string[] input = File.ReadAllLines(inputFile);

            return part.Run(input);
        }
    }
}
