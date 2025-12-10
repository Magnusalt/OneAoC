using AoC2025;

namespace AoC2025Tests;

[TestClass]
public class Day07Tests
{
    private readonly Day07 _sut = new();

    [TestMethod]
    public void Part1_Example()
    {
        var input = File.ReadAllLines("../../../../../Years/AoC2025/example/day07.txt");
        var result = _sut.RunPart1(input);
        Assert.AreEqual("21", result);
    }

    [TestMethod]
    public void Part2_Example()
    {
        var input = File.ReadAllLines("../../../../../Years/AoC2025/example/day07.txt");
        var result = _sut.RunPart2(input);
        Assert.AreEqual("40", result);
    }
}
