using Common;

namespace AoC2024;

public class Day01 : IDay
{
    public string RunPart1(string[] input)
    {
        var leftRight = input.Select(i => i.Split("   ")).Select(lr => (int.Parse(lr[0]), int.Parse(lr[1]))).ToList();
        var left = leftRight.Select(lr => lr.Item1).Order();
        var right = leftRight.Select(lr => lr.Item2).Order();
        return left.Zip(right, (l, r) => Math.Abs(l - r)).Sum().ToString();
    }

    public string RunPart2(string[] input)
    {
        var leftRight = input.Select(i => i.Split("   ")).Select(lr => (int.Parse(lr[0]), int.Parse(lr[1]))).ToList();
        var left = leftRight.Select(lr => lr.Item1);
        var right = leftRight.Select(lr => lr.Item2).ToList();

        var simScore = left.Sum(item => item * right.Count(i => i == item));

        return simScore.ToString();
    }
}