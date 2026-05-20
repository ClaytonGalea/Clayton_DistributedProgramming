using Microsoft.EntityFrameworkCore;
using Clayton_BookingMicroservice.Models;

namespace Clayton_BookingMicroservice.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; }
    }
}