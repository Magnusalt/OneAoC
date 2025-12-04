using System.Net;
using System.Text;

if (args.Length < 2)
{
    Console.WriteLine("Usage: scaffold <year> <day>");
    return;
}

var year = args[0];
if (!int.TryParse(args[1], out var dayNumber))
{
    Console.WriteLine("Day must be an integer.");
    return;
}

var day = dayNumber.ToString("00");

var repoRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", ".."));

var yearProjectName = $"AoC{year}"; // e.g. "adventofcode25"
var yearProjectPath = Path.Combine(repoRoot, "Years", yearProjectName);

var inputDir = Path.Combine(yearProjectPath, "input");
var exampleDir = Path.Combine(yearProjectPath, "example");
Directory.CreateDirectory(inputDir);
Directory.CreateDirectory(exampleDir);

var dayClassFile = Path.Combine(yearProjectPath, $"Day{day}.cs");
var inputFile = Path.Combine(inputDir, $"day{day}.txt");
var exampleFile = Path.Combine(exampleDir, $"day{day}.txt");

if (!File.Exists(dayClassFile))
{
    var ns = yearProjectName;

    var content = $$"""
                    using Common;

                    namespace {{ns}};

                    public class Day{{day}} : IDay
                    {
                        public string RunPart1(string[] input)
                        {
                            return "TODO";
                        }

                        public string RunPart2(string[] input)
                        {
                            return "TODO";
                        }
                    }
                    """.TrimStart();

    File.WriteAllText(dayClassFile, content, Encoding.UTF8);
    Console.WriteLine($"Created {dayClassFile}");
}
else
{
    Console.WriteLine($"{dayClassFile} already exists, skipping.");
}

if (!File.Exists(exampleFile))
{
    File.WriteAllText(exampleFile, string.Empty, Encoding.UTF8);
    Console.WriteLine($"Created {exampleFile}");
}
else
{
    Console.WriteLine($"{exampleFile} already exists, skipping.");
}

if (File.Exists(inputFile))
{
    Console.WriteLine($"{inputFile} already exists, not overwriting input.");
}
else
{
    var session = GetSessionToken(repoRoot);

    if (session is null)
    {
        Console.WriteLine("No AoC session token found (env AOC_SESSION or .aoc-session).");
        Console.WriteLine($"Created empty input file at {inputFile}");
        File.WriteAllText(inputFile, string.Empty, Encoding.UTF8);
    }
    else
    {
        try
        {
            var input = await DownloadAocInputAsync(year, dayNumber, session);
            File.WriteAllText(inputFile, input, Encoding.UTF8);
            Console.WriteLine($"Downloaded input for {year} Day {day} to {inputFile}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to download input: {ex.Message}");
            Console.WriteLine($"Created empty input file at {inputFile}");
            File.WriteAllText(inputFile, string.Empty, Encoding.UTF8);
        }
    }
}

GenerateUnitTest(repoRoot, year, day);

Console.WriteLine("Done.");

static string? GetSessionToken(string repoRoot)
{
    var sessionFile = Path.Combine(repoRoot, ".aoc-session");
    if (File.Exists(sessionFile))
    {
        var token = File.ReadAllText(sessionFile).Trim();
        if (!string.IsNullOrWhiteSpace(token))
            return token;
    }

    return null;
}

static async Task<string> DownloadAocInputAsync(string year, int day, string session)
{
    var url = $"https://adventofcode.com/{year}/day/{day}/input";

    var handler = new HttpClientHandler
    {
        CookieContainer = new CookieContainer()
    };

    handler.CookieContainer.Add(
        new Uri("https://adventofcode.com"),
        new Cookie("session", session)
    );

    using var client = new HttpClient(handler);
    client.DefaultRequestHeaders.UserAgent.ParseAdd("aoc-scaffolder (+https://example.com)");

    var response = await client.GetAsync(url);
    response.EnsureSuccessStatusCode();

    var text = await response.Content.ReadAsStringAsync();
    // AoC inputs usually end with a trailing newline
    return text.TrimEnd('\n', '\r');
}


static void GenerateUnitTest(string repoRoot, string year, string day)
{
    var testProjectName = $"AoC{year}Tests";
    var testProjectPath = Path.Combine(repoRoot, "Tests", testProjectName);
    Directory.CreateDirectory(testProjectPath);

    var testFile = Path.Combine(testProjectPath, $"Day{day}Tests.cs");
    if (File.Exists(testFile))
    {
        Console.WriteLine($"{testFile} already exists, skipping tests.");
        return;
    }

    var yearNamespace = $"AoC{year}";
    var testNamespace = $"{yearNamespace}Tests";

    var exampleRelative = Path.Combine("..", "..", "..", "..", "..", "Years", $"AoC{year}", "example", $"day{day}.txt");

    var testContent = $$"""
                        using {{yearNamespace}};

                        namespace {{testNamespace}};

                        [TestClass]
                        public class Day{{day}}Tests
                        {
                            private readonly Day{{day}} _sut = new();

                            [TestMethod]
                            public void Part1_Example()
                            {
                                var input = File.ReadAllLines("{{exampleRelative.Replace("\\", "\\\\")}}");
                                var result = _sut.RunPart1(input);
                                Assert.AreEqual("TODO", result);
                            }

                            [TestMethod]
                            public void Part2_Example()
                            {
                                var input = File.ReadAllLines("{{exampleRelative.Replace("\\", "\\\\")}}");
                                var result = _sut.RunPart2(input);
                                Assert.AreEqual("TODO", result);
                            }
                        }

                        """.TrimStart();

    File.WriteAllText(testFile, testContent, Encoding.UTF8);
    Console.WriteLine($"Created {testFile}");
}