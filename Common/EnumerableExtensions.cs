namespace Common;

public static class EnumerableExtensions
{
    public static (int dx, int dy)[] Directions8 =
    [
        (-1, -1), (0, -1), (1, -1),
        (-1, 0), /*current*/ (1, 0),
        (-1, 1), (0, 1), (1, 1)
    ];

    public static (int dx, int dy)[] Directions4 =
    [
        (0, -1),
        (-1, 0), /*current*/ (1, 0),
        (0, 1)
    ];

    extension(string[] input)
    {
        public char[][] ToCharMatrix()
        {
            return input.Select(row => row.ToCharArray()).ToArray();
        }

        public int[][] ToIntMatrix(char delimiter)
        {
            return input.Select(i => i.Split(delimiter, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray()).ToArray();
        }
    }

    extension(char[][] input)
    {
        public (int Rows, int Cols) Dimensions()
        {
            var rows = input.Length;
            var cols = input[0].Length;
            return (rows, cols);
        }

        public void ForEachNeighbour8(int x, int y, Action<char> action)
        {
            var (rows, cols) = input.Dimensions();

            foreach (var (dx, dy) in Directions8)
            {
                var nx = x + dx;
                var ny = y + dy;
                if (nx >= 0 && nx < cols && ny >= 0 && ny < rows)
                    action(input[ny][nx]);
            }
        }
    }
}