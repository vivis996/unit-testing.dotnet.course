using TestNinja.Fundamentals;

namespace TestNinja.Tests;

[TestFixture]
public class ErrorLoggerTests
{
    private ErrorLogger _logger;

    [SetUp]
    public void SetUp()
    {
        this._logger = new ErrorLogger();
    }

    [Test]
    public void Log_WhenCalled_SetTheLastErrorProperty()
    {
        _logger.Log("a");

        Assert.That(_logger.LastError, Is.EqualTo("a"));
    }
}

