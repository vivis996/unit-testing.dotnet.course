using System;
using TestNinja.Mocking;

namespace TestNinja.Tests.Mocking;

[TestFixture]
public class VideoServiceTests
{
    private VideoService _service;

    [SetUp]
    public void SetUp()
    {
        this._service = new VideoService();
    }

    [Test]
    public void ReadVideoTitle_EmptyFile_ReturnError()
    {
        var result = this._service.ReadVideoTitle(new FakeFileReader());

        Assert.That(result, Does.Contain("error").IgnoreCase);
    }
}
