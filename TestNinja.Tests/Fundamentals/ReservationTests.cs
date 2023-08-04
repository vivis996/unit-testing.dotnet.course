using TestNinja.Fundamentals;

namespace TestNinja.Tests.Fundamentals;

[TestFixture]
public class ReservationTests
{
    [Test]
    public void CanBeCancelledBy_UserIsAdmin_ReturnsTrue()
    {
        // Arrange
        var reservation = new Reservation();

        // Act
        var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void CanBeCancelledBy_SameUserCancellingTheReservation_ReturnsTrue()
    {
        var user = new User();
        var reservation = new Reservation { MadeBy = user };

        var result = reservation.CanBeCancelledBy(user);

        Assert.That(result == true);
    }

    [Test]
    public void CanBeCancelledBy_AnotherUserCancellingReservation_ReturnsFalse()
    {
        var user = new User();
        var reservation = new Reservation { MadeBy = user };

        var result = reservation.CanBeCancelledBy(new User());

        Assert.That(result, Is.False);
    }
}
