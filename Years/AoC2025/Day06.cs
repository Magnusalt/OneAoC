using Common;

namespace AoC2025;

public class Day06 : IDay
{
    public string RunPart1(string[] input)
    {
        var array = input[..^1].ToIntMatrix(' ');
        var operators = input[^1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

        long sum = 0;
        for (var i = 0; i < operators.Length; i++)
        {
            var op = operators[i];
            var i1 = i;
            var values = array.Select(v => (long)v[i1]);

            sum += op switch
            {
                "+" => values.Sum(),
                "*" => values.Aggregate((long)1, (s, n) => s * n)
            };
        }

        return sum.ToString();
    }

    public string RunPart2(string[] input)
    {
        var operators = new Stack<string>(input[^1].Split(' ', StringSplitOptions.RemoveEmptyEntries));
        var width = input[0].Length;
        long sum = 0;
        var op = operators.Pop();
        var values = new List<int>();

        for (int i = width - 1; i >= 0; i--)
        {
            var nbr = int.TryParse(new string(input[..^1].Select(v => v[i]).ToArray()), out var result) 
                ? result : 0;

            if (nbr == 0)
            {
                sum += op switch
                {
                    "+" => values.Sum(),
                    "*" => values.Aggregate((long)1, (s, n) => s * n)
                };
                
                op = operators.Pop();
                values.Clear();
                continue;
            }
            values.Add(nbr);
        }
        sum += op switch
        {
            "+" => values.Sum(),
            "*" => values.Aggregate((long)1, (s, n) => s * n)
        };
        
        return sum.ToString();
    }
}