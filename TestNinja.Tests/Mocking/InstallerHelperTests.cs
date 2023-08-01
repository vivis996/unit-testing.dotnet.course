using System.Net;
using Moq;
using TestNinja.Mocking;

namespace TestNinja.Tests.Mocking;

public class InstallerHelperTests
{
    private Mock<IFileDownloader> _fileDownloader;
    private InstallerHelper _installerHelper;

    [SetUp]
    public void SetUp()
    {
        this._fileDownloader = new Mock<IFileDownloader>();
        this._installerHelper = new InstallerHelper(this._fileDownloader.Object);
    }

    [Test]
    public void DownloadInstaller_DownloadFails_ReturnFalse()
    {
        this._fileDownloader.Setup(r => r.DownloadFile(It.IsAny<string>(), It.IsAny<string>()))
                            .Throws<WebException>();

        var result = this._installerHelper.DownloadInstaller("customer", "installer");

        Assert.That(result, Is.False);
    }

    [Test]
    public void DownloadInstaller_DownloadCompletes_ReturnTrue()
    {
        var result = this._installerHelper.DownloadInstaller("customer", "installer");

        Assert.That(result, Is.True);
    }
}
