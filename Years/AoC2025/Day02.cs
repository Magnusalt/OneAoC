using Common;

namespace AoC2025;

public class Day02 : IDay
{
    public string RunPart1(string[] input)
    {
        var ranges = HandleInput(input);

        long sum = 0;

        foreach (var range in ranges)
        {
            var half = range.Upper.FirstHalf;
            var nextRepeating = long.Parse($"{half}{half}");

            while (nextRepeating >= range.Lower.NumericValue)
            {
                if (nextRepeating <= range.Upper.NumericValue) sum += nextRepeating;
                half--;
                nextRepeating = long.Parse($"{half}{half}");
            }
        }

        return sum.ToString();
    }

    public string RunPart2(string[] input)
    {
        var ranges = HandleInput(input);

        long sum = 0;

        foreach (var range in ranges)
        {
            var invalidInRange = new HashSet<long>();
            foreach (var repetitions in range.Upper.GetRepetitions().Union(range.Lower.GetRepetitions()))
            {
                var groupSize = range.Lower.Length % repetitions == 0
                    ? range.Lower.Length / repetitions
                    : range.Upper.Length / repetitions;
                var group = (long)Math.Pow(10, groupSize - 1);

                var nextRepeating = MakeRepeated(group, repetitions);

                while (nextRepeating <= range.Upper.NumericValue)
                {
                    if (nextRepeating >= range.Lower.NumericValue) invalidInRange.Add(nextRepeating);
                    group++;

                    nextRepeating = MakeRepeated(group, repetitions);
                }
            }

            sum += invalidInRange.Sum();
        }

        return sum.ToString();
    }

    private long MakeRepeated(long group, int reps)
    {
        var s = group.ToString();
        var len = s.Length;

        long result = 0;
        var mul = (long)Math.Pow(10, len);

        for (var i = 0; i < reps; i++)
            result = result * mul + group;

        return result;
    }

    private List<Range> HandleInput(string[] input)
    {
        var line = input[0].Split(',').Select(r =>
        {
            var split = r.Split('-');
            return new Range(new Boundry(split[0]), new Boundry(split[1]));
        }).ToList();

        if (line.All(b => b.Lower.NumericValue <= b.Upper.NumericValue)) return line;
        throw new Exception("Oops this won't work!");
    }

    private record Range(Boundry Lower, Boundry Upper);

    private record Boundry(string Value)
    {
        public long NumericValue => long.Parse(Value);
        public int Length => Value.Length;
        public bool EvenLength => Length % 2 == 0;

        public long FirstHalf =>
            EvenLength ? long.Parse(Value[..(Length / 2)]) : long.Parse(Value[..((Length + 1) / 2)]);

        // Part 2 

        public IEnumerable<int> GetRepetitions()
        {
            for (var i = 2; i <= Length; i++)
                if (Length % i == 0)
                    yield return i;
        }
    }
}