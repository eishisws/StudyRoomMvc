using Microsoft.EntityFrameworkCore;
using StudyRoomMvc.Models;

namespace StudyRoomMvc.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Booking> Bookings { get; set; }
    }
}
