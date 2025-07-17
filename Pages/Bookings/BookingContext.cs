using Microsoft.EntityFrameworkCore;

namespace WebClient.Pages.Bookings
{
    public class BookingContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }

        public BookingContext(DbContextOptions options) : base(options)
        {

        }
    }

    public class Booking
    {

    }
}
