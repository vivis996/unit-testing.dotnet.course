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

    [SetUp]
    public void SetUp()
    {
        this._housekeeper = new Housekeeper { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" };

        var unitOfWork = new Mock<IUnitOfWork>();
        unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper>
        {
            this._housekeeper,
        }.AsQueryable());

        this._statementGenerator = new Mock<IStatementGenerator>();
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
}

