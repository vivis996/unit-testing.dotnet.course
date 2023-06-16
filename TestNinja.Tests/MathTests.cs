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
    public void Max_FirstArgumentIsGreater_ReturnTheFirstArgument()
    {
        var result = this._math.Max(2, 1);

        Assert.That(result, Is.EqualTo(2));
    }

    [Test]
    public void Max_SecondArgumentIsGreater_ReturnTheSecondArgument()
    {
        var result = this._math.Max(1, 2);

        Assert.That(result, Is.EqualTo(2));
    }

    [Test]
    public void Max_ArgumentsAreEqual_ReturnTheSameArgument()
    {
        var result = this._math.Max(1, 1);

        Assert.That(result, Is.EqualTo(1));
    }
}