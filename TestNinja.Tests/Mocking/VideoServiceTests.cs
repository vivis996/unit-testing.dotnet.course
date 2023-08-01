using Moq;
using TestNinja.Mocking;

namespace TestNinja.Tests.Mocking;

[TestFixture]
public class VideoServiceTests
{
    private VideoService _service;
    // Prefer use Mock for external dependencies
    private Mock<IFileReader> _fileReader;
    private Mock<IVideoRepository> _repository;

    [SetUp]
    public void SetUp()
    {
        this._fileReader = new Mock<IFileReader>();
        this._repository = new Mock<IVideoRepository>();
        this._service = new VideoService(this._fileReader.Object, this._repository.Object);
    }

    [Test]
    public void ReadVideoTitle_EmptyFile_ReturnError()
    {
        this._fileReader.Setup(fr => fr.Read("video.txt")).Returns("");
        var result = this._service.ReadVideoTitle();

        Assert.That(result, Does.Contain("error").IgnoreCase);
    }

    [Test]
    public void GetUnprocessedVideosAsCsv_AllVideosAreprocessedVideos_ReturnAnEmptyString()
    {
        this._repository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>());
        var result = this._service.GetUnprocessedVideosAsCsv();

        Assert.That(result, Is.EqualTo(string.Empty));
    }

    [Test]
    public void GetUnprocessedVideosAsCsv_AFewVideosAreprocessedVideos_ReturnAStringWithIdOfUnprocessedVideos()
    {
        this._repository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>()
        {
            new Video { Id = 1, },
            new Video { Id = 2, },
            new Video { Id = 3, },
        });
        var result = this._service.GetUnprocessedVideosAsCsv();

        Assert.That(result, Is.EqualTo("1,2,3"));
    }
}
