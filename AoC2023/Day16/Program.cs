using System.Diagnostics;
using Utils;

Stopwatch sw = Stopwatch.StartNew();

long result = AoC.Execute<PartOne, PartTwo, long>(Part.One, "input.txt");

sw.Stop();

Console.WriteLine(result);
Console.WriteLine($"Execution duration: {sw.Elapsed}");