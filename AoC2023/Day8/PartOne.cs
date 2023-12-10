using System.Text.RegularExpressions;
using Utils;

internal class PartOne : PartBase<long>
{
    public override long Run(string[] input)
    {
        Dictionary<string, Node> nodes = PrepareNodesWithInstruction(input, out string instructions);

        long result = 0;
        int idx = 0;

        var currentNode = nodes["AAA"];
        while(true)
        {
            if (currentNode.Key == "ZZZ")
                break;

            if (idx == instructions.Length)
                idx = 0;

            char instr = instructions[idx];

            if (instr == 'R')
                currentNode = nodes[currentNode.Right];
            else if (instr == 'L')
                currentNode = nodes[currentNode.Left];
            else
                throw new InvalidOperationException();

            result++;
            idx++;
        }

        return result;
    }

    public static Dictionary<string, Node> PrepareNodesWithInstruction(string[] input, out string instructions)
    {
        instructions = input[0];

        Dictionary<string, Node> nodes = new Dictionary<string, Node>();
        for (int i = 2; i < input.Length; i++)
        {
            string pattern = @"(\w+) = \((\w+), (\w+)\)";

            Regex regex = new Regex(pattern);

            Match match = regex.Match(input[i]);

            nodes.Add(match.Groups[1].Value, new Node
            {
                Key = match.Groups[1].Value,
                Left = match.Groups[2].Value,
                Right = match.Groups[3].Value
            });
        }

        return nodes;
    }

    public class Node
    {
        public string Key { get; set; }

        public string Left { get; set; }

        public string Right { get; set; }
    }
}