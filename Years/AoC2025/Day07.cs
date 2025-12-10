using Common;

namespace AoC2025;

public class Day07 : IDay
{
    public string RunPart1(string[] input)
    {
        var beamIndices = GetIndicesOf(input[0], 'S');

        var nbrOfSplits = 0;
        foreach (var row in input[1..])
        {
            var splitterIndices = GetIndicesOf(row, '^');
            if (splitterIndices.Count == 0)
            {
                continue;
            }

            var nextBeams = new HashSet<int>();
            foreach (var splitterIndex in splitterIndices.Where(splitterIndex => beamIndices.Contains(splitterIndex)))
            {
                nextBeams.Add(splitterIndex + 1);
                nextBeams.Add(splitterIndex - 1);
                beamIndices.Remove(splitterIndex);
                nbrOfSplits++;
            }

            foreach (var beamIndex in beamIndices)
            {
                nextBeams.Add(beamIndex);
            }

            beamIndices = nextBeams.ToList();
        }

        return nbrOfSplits.ToString();
    }

    public string RunPart2(string[] input)
    {
        var x = input[0].IndexOf('S');
        var memo = new Dictionary<(int x, int y), long>();

        var count = RecCount((x, 1), input, memo, 0);

        return count.ToString();
    }

    private static long RecCount((int x, int y) pos, string[] map, Dictionary<(int x, int y), long> memo, long sum)
    {
        while (map[pos.y][pos.x] == '.' && pos.y < map.Length - 1)
        {
            pos.y++;
        }

        if (pos.y == map.Length - 1)
        {
            return 1;
        }

        if (memo.ContainsKey((pos.x, pos.y)))
        {
            return memo[(pos.x, pos.y)];
        }

        var leftBranch = RecCount((pos.x - 1, pos.y), map, memo, sum);
        var rightBranch = RecCount((pos.x + 1, pos.y), map, memo, sum);

        memo.Add((pos.x, pos.y), leftBranch + rightBranch);

        return sum + leftBranch + rightBranch;
    }

    private List<int> GetIndicesOf(string s, char c)
    {
        var foundIndexes = new List<int>();
        for (var i = 0; i < s.Length; i++)
        {
            if (s[i] == c)
            {
                foundIndexes.Add(i);
            }
        }

        return foundIndexes;
    }
}