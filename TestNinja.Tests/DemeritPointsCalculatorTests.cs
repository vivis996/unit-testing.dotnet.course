using TestNinja.Fundamentals;

namespace TestNinja.Tests;

public class DemeritPointsCalculatorTests
{
    private DemeritPointsCalculator _calculator;

    [SetUp]
    public void SetUp()
    {
        this._calculator = new DemeritPointsCalculator();
    }

    [Test]
    [TestCase(-1)]
    [TestCase(301)]
    public void CalulateDeremitPoints_SpeedIsOutOfRange_ThrowArgumentOutOfRangeException(int speed)
    {
        Assert.That(() => this._calculator.CalculateDemeritPoints(speed), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
    }

    [Test]
    [TestCase(0, 0)]
    [TestCase(64, 0)]
    [TestCase(65, 0)]
    [TestCase(66, 0)]
    [TestCase(70, 1)]
    [TestCase(75, 2)]
    public void CalulateDeremitPoints_WhenCalled_ReturnDemeritPoints(int speed, int expectedResult)
    {
        var points = this._calculator.CalculateDemeritPoints(speed);
        Assert.That(points, Is.EqualTo(expectedResult));
    }
}

