using Math = TestNinja.Fundamentals.Math;

namespace TestNinja.Tests.Fundamentals;

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
    //[Ignore("Because I wanted to!")]
    public void Add_WhenCalled_ReturnTheSumsOfArguments()
    {
        var result = this._math.Add(1, 2);

        Assert.That(result, Is.EqualTo(3));
        //Assert.That(this._math, Is.Not.Null);
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

    [Test]
    public void GetOddNumbers_LimitIsGreaterThatnZero_ReturnOddNumbersUpToLimit()
    {
        var result = _math.GetOddNumbers(5);

        Assert.That(result, Is.Not.Empty);
        Assert.That(result.Count(), Is.EqualTo(3));

        //Assert.That(result, Does.Contain(1));
        //Assert.That(result, Does.Contain(3));
        //Assert.That(result, Does.Contain(5));

        Assert.That(result, Is.EquivalentTo(new[] { 1, 3, 5 }));

        //Assert.That(result, Is.Ordered);
        //Assert.That(result, Is.Unique);
    }
}