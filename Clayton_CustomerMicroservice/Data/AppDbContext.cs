using Microsoft.EntityFrameworkCore;
using Clayton_CustomerMicroservice.Models;

namespace Clayton_CustomerMicroservice.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Notification> Notifications { get; set; }
    }
}