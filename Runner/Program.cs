using System.Reflection;
using System.Text.RegularExpressions;
using Common;

var exeDir = AppContext.BaseDirectory;
var repoRoot = Path.GetFullPath(Path.Combine(exeDir, "..", "..", "..", ".."));
var yearsRoot = Path.Combine(repoRoot, "Years");

string? argYear = null;
string? argDay = null;

for (var i = 0; i < args.Length; i++)
{
    if (args[i] == "--year" && i + 1 < args.Length)
        argYear = args[i + 1];
    if (args[i] == "--day" && i + 1 < args.Length)
        argDay = args[i + 1].PadLeft(2, '0');
}

var yearFolders = Directory.GetDirectories(yearsRoot)
    .Where(f => Path.GetFileName(f).StartsWith("AoC"))
    .OrderBy(f => f)
    .ToList();

if (!yearFolders.Any())
{
    Console.WriteLine("No AoC year folders found under /Years.");
    return;
}

string selectedYearFolderName;

if (argYear != null)
{
    selectedYearFolderName = $"AoC{argYear}";
    if (!Directory.Exists(Path.Combine(yearsRoot, selectedYearFolderName)))
    {
        Console.WriteLine($"Year folder '{selectedYearFolderName}' not found.");
        return;
    }
}
else
{
    // default: last (latest) year
    selectedYearFolderName = Path.GetFileName(yearFolders.Last());
}

var selectedYearPath = Path.Combine(yearsRoot, selectedYearFolderName);

var inputDir = Path.Combine(selectedYearPath, "input");

if (!Directory.Exists(inputDir))
{
    Console.WriteLine($"Input directory missing: {inputDir}");
    return;
}

var inputFiles = Directory.GetFiles(inputDir, "day*.txt")
    .OrderBy(f => f, StringComparer.InvariantCulture)
    .ToList();

if (!inputFiles.Any())
{
    Console.WriteLine($"No input files found in {inputDir}");
    return;
}

string dayNumber;

if (argDay != null)
{
    dayNumber = argDay;
}
else
{
    // Example: lastInput = ".../day03.txt"
    var lastInput = inputFiles.Last();
    var fileName = Path.GetFileNameWithoutExtension(lastInput);
    var match = Regex.Match(fileName, @"\d+");
    if (!match.Success)
    {
        Console.WriteLine($"Failed to extract day number from {fileName}");
        return;
    }

    dayNumber = match.Value.PadLeft(2, '0');
}

var inputPath = Path.Combine(inputDir, $"day{dayNumber}.txt");
if (!File.Exists(inputPath))
{
    Console.WriteLine($"Input file not found: {inputPath}");
    return;
}

Console.WriteLine($"Running {selectedYearFolderName} Day {dayNumber}");
Console.WriteLine($"Input: {inputPath}");

var input = File.ReadAllLines(inputPath);

var yearNamespace = selectedYearFolderName;
var typeName = $"{yearNamespace}.Day{dayNumber}";

var assembly = Assembly.Load(yearNamespace);
var type = assembly.GetType(typeName);

if (type == null)
{
    Console.WriteLine($"Type not found: {typeName}");
    return;
}

var solver = (IDay)Activator.CreateInstance(type)!;

Console.WriteLine();
Console.WriteLine("Part 1:");
Console.WriteLine(solver.RunPart1(input));

Console.WriteLine();
Console.WriteLine("Part 2:");
Console.WriteLine(solver.RunPart2(input));
Console.WriteLine();