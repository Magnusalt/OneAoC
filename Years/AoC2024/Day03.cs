using System.Text.RegularExpressions;
using Common;

namespace AoC2024;

public partial class Day03 : IDay
{
    public string RunPart1(string[] input)
    {
        var instructions = new List<string>();

        foreach (var row in input)
        {
            var matches = FindMul().Matches(row);
            instructions.AddRange(matches.Select(m => m.ToString()));
        }

        return instructions
            .Select(i => i[4..^1].Split(',')
                .Select(int.Parse)
                .Aggregate(1, (acc, val) => acc * val))
            .Sum()
            .ToString();
    }

    public string RunPart2(string[] input)
    {
        var enable = true;
        var sum = 0;
        foreach (var row in input)
        {
            var between = FindMul().EnumerateSplits(row);
            var matches = FindMul().Matches(row);
            var matchIndex = 0;
            foreach (var section in between)
            {
                if (section.Start.Value == section.End.Value && section.End.Value == row.Length) continue;
                var before = row[section.Start..section.End];
                var foundDo = FindDo().IsMatch(before);
                var foundDont = FindDont().IsMatch(before);

                enable = (enable, foundDo, foundDont) switch
                {
                    (true, true, false) => true,
                    (true, false, true) => false,
                    (false, true, false) => true,
                    (false, false, true) => false,
                    (true, false, false) => true,
                    (false, false, false) => false,
                    (_, _, _) => throw new InvalidOperationException(
                        $"enable: {enable}, foundDo: {foundDo}, foundDont: {foundDont} ")
                };

                if (enable && matchIndex < matches.Count)
                {
                    var matchValue = matches[matchIndex].ToString()[4..^1]
                        .Split(',')
                        .Select(int.Parse)
                        .Aggregate(1, (acc, val) => acc * val);
                    
                    sum += matchValue;
                }

                matchIndex++;
            }
        }


        return sum.ToString();
    }

    [GeneratedRegex("mul\\([0-9]+\\,[0-9]+\\)")]
    private static partial Regex FindMul();

    [GeneratedRegex("do\\(\\)")]
    private static partial Regex FindDo();

    [GeneratedRegex("don\\'t\\(\\)")]
    private static partial Regex FindDont();
}