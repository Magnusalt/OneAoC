using AoC2024;

namespace AoC2024Tests;

[TestClass]
public class Day03Tests
{
    private readonly Day03 _sut = new();

    [TestMethod]
    public void Part1_Example()
    {
        var input = File.ReadAllLines("../../../../../Years/AoC2024/example/day03.txt");
        var result = _sut.RunPart1(input);
        Assert.AreEqual("161", result);
    }

    [TestMethod]
    public void Part2_Example()
    {
        var input = File.ReadAllLines("../../../../../Years/AoC2024/example/day03_2.txt");
        var result = _sut.RunPart2(input);
        Assert.AreEqual("48", result);
    }
}
