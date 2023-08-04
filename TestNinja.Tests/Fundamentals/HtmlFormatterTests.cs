using TestNinja.Fundamentals;

namespace TestNinja.Tests.Fundamentals;

[TestFixture]
public class HtmlFormatterTests
{
    [Test]
    public void FormatAsBold_WhenCalled_ShouldEcnloseTheStringWithStrongElement()
    {
        var formatter = new HtmlFormatter();

        var result = formatter.FormatAsBold("abc");

        // Specific
        Assert.That(result, Is.EqualTo("<strong>abc</strong>").IgnoreCase);

        // More general
        Assert.That(result, Does.StartWith("<strong>").IgnoreCase);
        Assert.That(result, Does.EndWith("</strong>"));
        Assert.That(result, Does.Contain("abc"));
    }
}
