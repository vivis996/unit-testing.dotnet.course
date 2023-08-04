using Moq;
using TestNinja.Mocking;

namespace TestNinja.Tests.Mocking;

[TestFixture]
public class HouseKeeperServiceTests
{
    private HousekeeperService _service;
    private Mock<IStatementGenerator> _statementGenerator;
    private Mock<IEmailSender> _emailSender;
    private Mock<IXtraMessageBox> _messageBox;
    private Housekeeper _housekeeper;
    private readonly DateTime _statementDate = new DateTime(2017, 1, 1);
    private string _statementFileName;

    [SetUp]
    public void SetUp()
    {
        this._housekeeper = new Housekeeper { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" };

        var unitOfWork = new Mock<IUnitOfWork>();
        unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
        {
            this._housekeeper,
        }.AsQueryable());

        _statementFileName = "fileName";

        this._statementGenerator = new Mock<IStatementGenerator>();
        this._statementGenerator
                .Setup(sg => sg.SaveStatement(this._housekeeper.Oid, this._housekeeper.FullName, this._statementDate))
                .Returns(() => _statementFileName);

        this._emailSender = new Mock<IEmailSender>();
        this._messageBox = new Mock<IXtraMessageBox>();

        this._service = new HousekeeperService(unitOfWork.Object, this._statementGenerator.Object,
                                             this._emailSender.Object, this._messageBox.Object);
    }

    [Test]
    public void SendStatementEmails_WhenCalled_GenerateStatements()
    {
        this._service.SendStatementEmails(this._statementDate);

        this._statementGenerator.Verify(sg => sg.SaveStatement(this._housekeeper.Oid, this._housekeeper.FullName, this._statementDate));
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void SendStatementEmails_HouseKeeperEmailIsNotValid_ShouldNotGenerateStatement(string houseKeeperEmail)
    {
        this._housekeeper.Email = houseKeeperEmail;

        this._service.SendStatementEmails(this._statementDate);

        this._statementGenerator.Verify(sg => sg.SaveStatement(this._housekeeper.Oid, this._housekeeper.FullName, this._statementDate), Times.Never);
    }

    [Test]
    public void SendStatementEmails_WhenCalled_EmailTheStatement()
    {
        this._service.SendStatementEmails(this._statementDate);

        VerifyEmailSent();
    }

    [Test]
    [TestCase(null)]
    [TestCase("")]
    [TestCase(" ")]
    public void SendStatementEmails_ThereIsNoStatementFileName_ShouldNotEmailTheStatement(string fileName)
    {
        this._statementFileName = fileName;
        this._service.SendStatementEmails(this._statementDate);
        VerifyEmailNotSent();
    }

    [Test]
    public void SendStatementEmails_EmailSendingFails_DisplayAMessageBox()
    {
        this._emailSender
                .Setup(es => es.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws<Exception>();

        this._service.SendStatementEmails(this._statementDate);

        this._messageBox.Verify(mb => mb.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
    }

    private void VerifyEmailNotSent()
    {
        this._emailSender
                        .Verify(es => es.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()),
                                Times.Never);
    }

    private void VerifyEmailSent()
    {
        this._emailSender
                .Verify(es =>
                           es.EmailFile(this._housekeeper.Email, this._housekeeper.StatementEmailBody, this._statementFileName, It.IsAny<string>()));
    }
}
