using TestNinja.Fundamentals;

namespace TestNinja.Tests.Fundamentals;

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
        this._logger.Log("a");

        Assert.That(this._logger.LastError, Is.EqualTo("a"));
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void Log_IvalidError_ThrowArgumentNullException(string error)
    {
        Assert.That(() => this._logger.Log(error), Throws.ArgumentNullException);
    }

    [Test]
    public void Log_ValidError_RaiseErrorLoggedEvent()
    {
        var id = Guid.Empty;
        this._logger.ErrorLogged += (sender, args) => id = args;

        this._logger.Log("a");

        Assert.That(id, Is.Not.EqualTo(Guid.Empty));
    }
}
