using Math = TestNinja.Fundamentals.Math;

namespace TestNinja.Tests;

[TestFixture]
public class MathTests
{
    [Test]
    public void Add_WhenCalled_ReturnTheSumsOfArguments()
    {
        var math = new Math();

        var result = math.Add(1, 2);

        Assert.That(result, Is.EqualTo(3));
    }
}