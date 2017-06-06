using System.Data.Entity;

namespace TourRun.Models
{
    public class TourContext : DbContext
    {
        public TourContext() : base("TourContext")
        {
        }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Transporter> Transporters { get; set; }
        public DbSet<Transport> Transports { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}