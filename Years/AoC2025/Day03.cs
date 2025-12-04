using Common;

namespace AoC2025;

public class Day03 : IDay
{
    public string RunPart1(string[] input)
    {
        var sum = 0;
        foreach (var battery in input)
        {
            var maxTen = battery[..^1].Max();
            var maxIndex = battery.IndexOf(maxTen);
            var maxOne = battery[(maxIndex + 1)..].Max();
            var value = int.Parse($"{maxTen}{maxOne}");
            sum += value;
        }

        return sum.ToString();
    }

    public string RunPart2(string[] input)
    {
        long sum = 0;
        foreach (var battery in input)
        {
            var res = new char[12];
            var offset = 0;
            for (var i = 1; i <= 12; i++)
            {
                var slice = battery[offset..^(12 - i)];
                var max = slice.Max();
                offset += slice.IndexOf(max) + 1;
                res[i - 1] = max;
            }

            var value = long.Parse(new string(res));
            sum += value;
        }

        return sum.ToString();
    }
}