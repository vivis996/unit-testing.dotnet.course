﻿using System;

namespace TestNinja.Mocking;

public class BookingRepository : IBookingRepository
{
    public IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null)
    {
        var unitOfWork = new UnitOfWork();
        var bookings =
            unitOfWork.Query<Booking>()
                .Where(b => b.Status != "Cancelled");

        if (excludedBookingId.HasValue)
            bookings = bookings.Where(b => b.Id != excludedBookingId);

        return bookings;
    }
}

public interface IBookingRepository
{
    IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null);
}
