using AoC2025;

namespace AoC2025Tests;

[TestClass]
public class Day05Tests
{
    private readonly Day05 _sut = new();

    [TestMethod]
    public void Part1_Example()
    {
        var input = File.ReadAllLines("../../../../../Years/AoC2025/example/day05.txt");
        var result = _sut.RunPart1(input);
        Assert.AreEqual("3", result);
    }

    [TestMethod]
    public void Part2_Example()
    {
        var input = File.ReadAllLines("../../../../../Years/AoC2025/example/day05.txt");
        var result = _sut.RunPart2(input);
        Assert.AreEqual("14", result);
    }
}
