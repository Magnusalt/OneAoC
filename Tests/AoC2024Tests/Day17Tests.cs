using AoC2024;

namespace AoC2024Tests;

[TestClass]
public class Day17Tests
{
    private readonly Day17 _sut = new();

    [TestMethod]
    public void Part1_Example()
    {
        var input = File.ReadAllLines("../../../../../Years/AoC2024/example/day17.txt");
        var result = _sut.RunPart1(input);
        Assert.AreEqual("TODO", result);
    }

    [TestMethod]
    public void Part2_Example()
    {
        var input = File.ReadAllLines("../../../../../Years/AoC2024/example/day17.txt");
        var result = _sut.RunPart2(input);
        Assert.AreEqual("TODO", result);
    }
}
