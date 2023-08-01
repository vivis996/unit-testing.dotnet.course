using Moq;
using TestNinja.Mocking;

namespace TestNinja.Tests.Mocking;

[TestFixture]
public class VideoServiceTests
{
    private VideoService _service;
    // Prefer use Mock for external dependencies
    private Mock<IFileReader> _fileReader;

    [Test]
    public void ReadVideoTitle_EmptyFile_ReturnError()
    {
        this._fileReader = new Mock<IFileReader>();
        _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");
        this._service = new VideoService(this._fileReader.Object);
        var result = this._service.ReadVideoTitle();

        Assert.That(result, Does.Contain("error").IgnoreCase);
    }
}
