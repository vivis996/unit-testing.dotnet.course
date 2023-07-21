using Moq;
using TestNinja.Mocking;

namespace TestNinja.Tests.Mocking;

[TestFixture]
public class VideoServiceTests
{
    private VideoService _service;
    // Prefer use Mock for external dependencies
    private Mock<IFileReader> _fileReader;

    [SetUp]
    public void SetUp()
    {
        this._fileReader = new Mock<IFileReader>();
        this._service = new VideoService(this._fileReader.Object);
    }

    [Test]
    public void ReadVideoTitle_EmptyFile_ReturnError()
    {
        _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");
        var result = this._service.ReadVideoTitle();

        Assert.That(result, Does.Contain("error").IgnoreCase);
    }
}
