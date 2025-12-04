using AoC2025;

namespace AoC2025Tests;

[TestClass]
public class Day01Tests
{
    private readonly Day01 _sut = new();

    [TestMethod]
    public void Part1_Example()
    {
        var input = File.ReadAllLines("../../../../../Years/AoC2025/example/day01.txt");
        var result = _sut.RunPart1(input);
        Assert.AreEqual("3", result);
    }

    [TestMethod]
    public void Part2_Example()
    {
        var input = File.ReadAllLines("../../../../../Years/AoC2025/example/day01.txt");
        var result = _sut.RunPart2(input);
        Assert.AreEqual("6", result);
    }
}
