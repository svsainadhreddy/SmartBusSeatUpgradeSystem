using BusSeatManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace BusSeatManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Bus> Buses { get; set; }

        public DbSet<Seat> Seats { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<SeatTransaction> SeatTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Seat)
                .WithMany()
                .HasForeignKey(b => b.CurrentSeatId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Booking>()
                .Property(b => b.OriginalFare)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Booking>()
                .Property(b => b.CurrentFare)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Seat>()
                .Property(s => s.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<SeatTransaction>()
                .Property(t => t.OldFare)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<SeatTransaction>()
                .Property(t => t.NewFare)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<SeatTransaction>()
                .Property(t => t.DifferenceAmount)
                .HasColumnType("decimal(18,2)");
        }
    }
}