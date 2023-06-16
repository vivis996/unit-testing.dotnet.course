using Math = TestNinja.Fundamentals.Math;

namespace TestNinja.Tests;

[TestFixture]
public class MathTests
{
    private Math _math;

    // SetUp
    // TearDown

    [SetUp]
    public void SetUp()
    {
        this._math = new Math();
    }

    [Test]
    public void Add_WhenCalled_ReturnTheSumsOfArguments()
    {
        var result = this._math.Add(1, 2);

        Assert.That(result, Is.EqualTo(3));
    }

    [Test]
    [TestCase(2, 1, 2)]
    [TestCase(1, 2, 2)]
    [TestCase(1, 1, 1)]
    public void Max_WhenCalled_ReturnTheGreaterArgument(int a, int b, int expectedResult)
    {
        var result = this._math.Max(a, b);

        Assert.That(result, Is.EqualTo(expectedResult));
    }
}