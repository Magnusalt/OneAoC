using Common;

namespace AoC2025;

public class Day01 : IDay
{
    public string RunPart1(string[] input)
    {
        var reduced = input.Aggregate((0, 50), (s, n) =>
        {
            var dir = n[..1] switch
            {
                "R" => 1,
                "L" => -1
            };
            var steps = int.Parse(n[1..]) % 100;
            var nextPos = s.Item2 + dir * steps;

            if (nextPos > 100)
            {
                nextPos -= 100;
                return (s.Item1, nextPos);
            }

            if (nextPos < 0)
            {
                nextPos += 100;
                return (s.Item1, nextPos);
            }

            if (nextPos == 0 || nextPos == 100)
            {
                nextPos = 0;
                return (s.Item1 + 1, nextPos);
            }

            return (s.Item1, nextPos);
        });

        return reduced.Item1.ToString();
    }

    public string RunPart2(string[] input)
    {
        var reduced2 = input.Aggregate((0, 50), (s, n) =>
        {
            var dir = n[..1] switch
            {
                "R" => 1,
                "L" => -1
            };
            var steps = int.Parse(n[1..]);
            var nextPos = s.Item2 switch
            {
                0 when dir > 0 => 0,
                0 when dir < 0 => 100,
                100 when dir > 0 => 0,
                100 when dir < 0 => 100,
                _ => s.Item2
            };

            var zeros = 0;
            for (var i = 0; i < steps; i++)
            {
                nextPos += dir;
                if (dir > 0 && nextPos == 100)
                {
                    nextPos = 0;
                    zeros++;
                }

                if (dir < 0 && nextPos == 0)
                {
                    nextPos = 100;
                    zeros++;
                }
            }

            return (s.Item1 + zeros, nextPos);
        });

        return reduced2.Item1.ToString();
    }
}