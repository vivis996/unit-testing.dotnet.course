﻿using Moq;
using TestNinja.Mocking;

namespace TestNinja.Tests.Mocking;

[TestFixture]
public class Booking_OverlappingBookingsExistHelperTests
{
    private Booking _existingBooking;
    private Mock<IBookingRepository> _repository;

    [SetUp]
    public void SetUp()
    {
        this._existingBooking = new Booking
        {
            Id = 2,
            ArrivalDate = ArriveOn(2017, 1, 15),
            DepartureDate = DepartOn(2017, 1, 20),
            Reference = "a"
        };

        this._repository = new Mock<IBookingRepository>();

        this._repository.Setup(r => r.GetActiveBookings(1)).Returns(new List<Booking>
        {
            this._existingBooking,
        }.AsQueryable());
    }

    [Test]
    public void BookingStartsAndFinishesBeforeAnExistingBooking_ReturnEmptyString()
    {
        var result = BookingHelper.OverlappingBookingsExist(new Booking
        {
            Id = 1,
            ArrivalDate = Before(this._existingBooking.ArrivalDate, days: 2),
            DepartureDate = Before(this._existingBooking.ArrivalDate),
        }, this._repository.Object);

        Assert.That(result, Is.Empty);
    }

    [Test]
    public void BookingStartsBeforeAndFinishesInTheMiddleOfAnExistingBooking_ReturnExistingBookingReferences()
    {
        var result = BookingHelper.OverlappingBookingsExist(new Booking
        {
            Id = 1,
            ArrivalDate = Before(this._existingBooking.ArrivalDate),
            DepartureDate = After(this._existingBooking.ArrivalDate),
        }, this._repository.Object);

        Assert.That(result, Is.EqualTo(this._existingBooking.Reference));
    }

    [Test]
    public void BookingStartsBeforeAndFinishesAfterOfAnExistingBooking_ReturnExistingBookingReferences()
    {
        var result = BookingHelper.OverlappingBookingsExist(new Booking
        {
            Id = 1,
            ArrivalDate = Before(this._existingBooking.ArrivalDate),
            DepartureDate = After(this._existingBooking.DepartureDate),
        }, this._repository.Object);

        Assert.That(result, Is.EqualTo(this._existingBooking.Reference));
    }

    [Test]
    public void BookingStartsAndFinishesInTheMiddleOfAnExistingBooking_ReturnExistingBookingReferences()
    {
        var result = BookingHelper.OverlappingBookingsExist(new Booking
        {
            Id = 1,
            ArrivalDate = After(this._existingBooking.ArrivalDate),
            DepartureDate = Before(this._existingBooking.DepartureDate),
        }, this._repository.Object);

        Assert.That(result, Is.EqualTo(this._existingBooking.Reference));
    }

    [Test]
    public void BookingStartsInTheMiddleAndFinishesAfterOfAnExistingBooking_ReturnExistingBookingReferences()
    {
        var result = BookingHelper.OverlappingBookingsExist(new Booking
        {
            Id = 1,
            ArrivalDate = After(this._existingBooking.ArrivalDate),
            DepartureDate = After(this._existingBooking.DepartureDate),
        }, this._repository.Object);

        Assert.That(result, Is.EqualTo(this._existingBooking.Reference));
    }

    [Test]
    public void BookingStartsAndFinishesAfterOfAnExistingBooking_ReturnEmptyString()
    {
        var result = BookingHelper.OverlappingBookingsExist(new Booking
        {
            Id = 1,
            ArrivalDate = After(this._existingBooking.DepartureDate),
            DepartureDate = After(this._existingBooking.DepartureDate, 2),
        }, this._repository.Object);

        Assert.That(result, Is.Empty);
    }

    [Test]
    public void BookingsOverlapButNewBookingIsCancelled_ReturnEmptyString()
    {
        var result = BookingHelper.OverlappingBookingsExist(new Booking
        {
            Id = 1,
            ArrivalDate = After(this._existingBooking.ArrivalDate),
            DepartureDate = After(this._existingBooking.DepartureDate),
            Status = "Cancelled",
        }, this._repository.Object);

        Assert.That(result, Is.Empty);
    }

    private DateTime Before(DateTime dateTime, int days = 1)
    {
        return dateTime.AddDays(-days);
    }

    private DateTime After(DateTime dateTime, int days = 1)
    {
        return dateTime.AddDays(days);
    }

    private DateTime ArriveOn(int year, int month, int day)
    {
        return new DateTime(year, month, day, 14, 0, 0);
    }

    private DateTime DepartOn(int year, int month, int day)
    {
        return new DateTime(year, month, day, 10, 0, 0);
    }
}
