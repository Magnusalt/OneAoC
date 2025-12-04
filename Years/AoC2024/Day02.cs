using Common;

namespace AoC2024;

public class Day02 : IDay
{
    public string RunPart1(string[] input)
    {
        var count = 0;

        foreach (var report in input)
        {
            var reportInts = report.Split(" ").Select(int.Parse).ToList();

            if (ReportIsSafe(reportInts)) count++;
        }

        return count.ToString();
    }


    public string RunPart2(string[] input)
    {
        var count = 0;

        foreach (var report in input)
        {
            var reportInts = report.Split(" ").Select(int.Parse).ToList();

            if (ReportIsSafe(reportInts))
            {
                count++;
                continue;
            }

            for (var indexToSkip = 0; indexToSkip < reportInts.Count; indexToSkip++)
            {
                var first = reportInts[..indexToSkip];
                var second = reportInts[(indexToSkip + 1)..];

                var reduced = first.Concat(second).ToList();

                if (ReportIsSafe(reduced))
                {
                    count++;
                    break;
                }
            }
        }

        return count.ToString();
    }

    private bool ReportIsSafe(List<int> report)
    {
        var initialDirection = report[1] - report[0];

        if (initialDirection == 0) return false;
        var increasing = initialDirection > 0;

        var level = 0;
        for (var i = 1; i < report.Count; i++)
        {
            var direction = report[i] - report[i - 1];
            var delta = Math.Abs(direction);
            if (!(delta >= 1 && delta <= 3)) return false;
            var nextLevel = level + direction;
            if (increasing)
            {
                if (nextLevel < level) return false;
            }
            else
            {
                if (nextLevel > level) return false;
            }
        }

        return true;
    }
}