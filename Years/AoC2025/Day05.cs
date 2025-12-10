using Common;
using Microsoft.VisualBasic;

namespace AoC2025;

public class Day05 : IDay
{
    public string RunPart1(string[] input)
    {
        var idRanges = new List<IngredientRange>();
        var i = 0;
        while (input[i] != string.Empty)
        {
            var range = input[i].Split('-');
            idRanges.Add(new(long.Parse(range[0]), long.Parse(range[1])));
            i++;
        }
        i++;

        var count = 0;
        foreach (var id in input[i..])
        {
            var idAsLong = long.Parse(id);

            if (idRanges.Any(ir => ir.IsInRange(idAsLong)))
            {
                count++;
            }
        }

        return count.ToString();
    }

    public string RunPart2(string[] input)
    {
        var indexOfBreak = input.IndexOf(string.Empty);

        var ranges = new List<IngredientRange>();
        foreach (var range in input[..indexOfBreak])
        {
            var parts = range.Split('-');
            ranges.Add(new(long.Parse(parts[0]), long.Parse(parts[1])));
        }

        var rangesWithoutOverlap = new List<IngredientRange>();

        var sortedRanges = ranges.OrderBy(r => r.Lower).ToList();

        var tail = sortedRanges.Skip(1).ToList();

        var head = sortedRanges.First();
        
        while (tail.Count != 0)
        {
            var currentLow = head.Lower;
            var currentHigh = head.Upper;
            foreach (var range in tail)
            {
                if (range.Lower <= currentHigh && range.Upper > currentHigh)
                {
                    currentHigh = range.Upper;
                }
            }
            rangesWithoutOverlap.Add(new(currentLow, currentHigh));
            head = tail.FirstOrDefault(r=> r.Lower > currentHigh);
            tail = tail.Where(r=> r.Lower > currentHigh).ToList();
        }

        return rangesWithoutOverlap.Sum(r => r.InclusiveDelta).ToString();
    }

    private record IngredientRange(long Lower, long Upper)
    {
        public bool IsInRange(long value)
        {
            return value >= Lower && value <= Upper;
        }

        public long InclusiveDelta => Upper - Lower + 1;
    }
}