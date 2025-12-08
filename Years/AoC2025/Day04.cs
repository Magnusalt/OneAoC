using Common;

namespace AoC2025;

public class Day04 : IDay
{
    public string RunPart1(string[] input)
    {
        var grid = input.ToCharMatrix();
        var nbrOfAccessibleRolls = 0;

        for (var y = 0; y < input.Length; y++)
        {
            var row = input[y];
            for (var x = 0; x < row.Length; x++)
            {
                var p = row[x];

                if (p == '.') 
                    continue;

                var nbrOfAdjacentRolls = 0;
                grid.ForEachNeighbour8(x, y, c =>
                {
                    if (c == '@')
                        nbrOfAdjacentRolls++;
                });

                nbrOfAccessibleRolls += nbrOfAdjacentRolls < 4 ? 1 : 0;
            }
        }

        return nbrOfAccessibleRolls.ToString();
    }

    public string RunPart2(string[] input)
    {
        var currentArrangement = input.ToList();
        var total = 0;
        while (true)
        {
            var nbrOfAccessibleRolls = 0;
            var maxY = currentArrangement.Count - 1;
            var maxX = currentArrangement[0].Length - 1;
            var y = 0;
            var markedForRemoval = new List<(int x, int y)>();

            foreach (var row in currentArrangement)
            {
                var x = 0;
                foreach (var p in row)
                {
                    if (p == '@')
                    {
                        var startY = y > 0 ? y - 1 : y;
                        var endY = y < maxY ? y + 1 : y;
                        var startX = x > 0 ? x - 1 : x;
                        var endX = x < maxX ? x + 1 : x;

                        var nbrOfAdjacentRolls = 0;
                        for (var i_y = startY; i_y <= endY; i_y++)
                        for (var i_x = startX; i_x <= endX; i_x++)
                        {
                            if (i_x == x && i_y == y) continue;
                            var adjacent = currentArrangement[i_y][i_x];
                            nbrOfAdjacentRolls += adjacent == '@' ? 1 : 0;
                        }

                        if (nbrOfAdjacentRolls < 4)
                        {
                            nbrOfAccessibleRolls++;
                            markedForRemoval.Add((x, y));
                        }
                    }

                    x++;
                }

                y++;
            }

            if (nbrOfAccessibleRolls == 0) break;

            total += nbrOfAccessibleRolls;

            var y2 = 0;
            var nextArrangement = new List<string>();
            foreach (var row in currentArrangement)
            {
                var rollsToRemove = markedForRemoval.Where(p => p.y == y2).Select(p => p.x).ToList();
                if (rollsToRemove.Count != 0)
                {
                    var nextRow = row.ToArray();
                    foreach (var roll in rollsToRemove) nextRow[roll] = '.';
                    nextArrangement.Add(new string(nextRow));
                }
                else
                {
                    nextArrangement.Add(row);
                }

                y2++;
            }

            currentArrangement = nextArrangement;
        }

        return total.ToString();
    }
}