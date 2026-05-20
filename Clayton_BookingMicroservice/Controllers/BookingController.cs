using Microsoft.AspNetCore.Mvc;
using Clayton_BookingMicroservice.Data;
using Clayton_BookingMicroservice.Models;
using Clayton_BookingMicroservice.DTOs;

namespace Clayton_BookingMicroservice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BookingController(AppDbContext context)
        {
            _context = context;
        }

        // CREATE BOOKING
        [HttpPost]
        public IActionResult CreateBooking(CreateBookingDto dto)
        {
            var booking = new Booking
            {
                UserId = dto.UserId,
                StartLocation = dto.StartLocation,
                EndLocation = dto.EndLocation,
                BookingDateTime = dto.BookingDateTime,
                PassengerCount = dto.PassengerCount,
                CabType = dto.CabType,
                Status = "Current"
            };

            _context.Bookings.Add(booking);
            _context.SaveChanges();

            return Ok(booking);
        }

        // VIEW CURRENT BOOKINGS
        [HttpGet("current/{userId}")]
        public IActionResult GetCurrentBookings(int userId)
        {
            var bookings = _context.Bookings
                .Where(x =>
                    x.UserId == userId &&
                    x.BookingDateTime >= DateTime.Now)
                .OrderBy(x => x.BookingDateTime)
                .ToList();

            return Ok(bookings);
        }

        // VIEW PAST BOOKINGS
        [HttpGet("past/{userId}")]
        public IActionResult GetPastBookings(int userId)
        {
            var bookings = _context.Bookings
                .Where(x =>
                    x.UserId == userId &&
                    x.BookingDateTime < DateTime.Now)
                .OrderByDescending(x => x.BookingDateTime)
                .ToList();

            return Ok(bookings);
        }
    }
}