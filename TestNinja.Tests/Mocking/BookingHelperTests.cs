using Moq;
using TestNinja.Mocking;

namespace TestNinja.Tests.Mocking;

[TestFixture]
public class Booking_OverlappingBookingsExistHelperTests
{
    [Test]
    public void BookingStartsAndFinishesBeforeAnExistingBooking_ReturnEmptyString()
    {
        var repository = new Mock<IBookingRepository>();
        repository.Setup(r => r.GetActiveBookings(1)).Returns(new List<Booking>
        {
            new Booking
            {
                Id = 2,
                ArrivalDate = new DateTime(2017, 1, 15, 14, 0, 0),
                DepartureDate = new DateTime(2017, 1, 20, 10, 0, 0),
                Reference = "a"
            },
        }.AsQueryable());

        var result = BookingHelper.OverlappingBookingsExist(new Booking
        {
            Id = 1,
            ArrivalDate = new DateTime(2017, 1, 10, 14, 0, 0),
            DepartureDate = new DateTime(2017, 1, 14, 10, 0, 0),
        }, repository.Object);

        Assert.That(result, Is.Empty);
    }
}
