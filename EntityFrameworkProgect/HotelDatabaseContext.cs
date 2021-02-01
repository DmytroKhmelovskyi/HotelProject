using Hotel.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkProgect
{
    public partial class HotelDatabaseContext : DbContext
    {
        public HotelDatabaseContext()
        {
        }

        public HotelDatabaseContext(DbContextOptions<HotelDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Guest> Guests { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomStatus> RoomStatuses { get; set; }
        public virtual DbSet<RoomType> RoomTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=HotelDatabase;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Guest>(entity =>
            {
                entity.HasIndex(e => e.Phone, "UQ__Guests__5C7E359E6BCB5E46")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Guests__A9D10534785B8791")
                    .IsUnique();

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PayTime).HasColumnType("datetime");

                entity.HasOne(d => d.Guest)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.GuestId)
                    .HasConstraintName("FK__Payments__GuestI__32E0915F");

                entity.HasOne(d => d.Reservation)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.ReservationId)
                    .HasConstraintName("FK__Payments__Reserv__33D4B598");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.Property(e => e.CheckInDate).HasColumnType("datetime");

                entity.Property(e => e.CheckOutDate).HasColumnType("datetime");

                entity.Property(e => e.ReservationDate).HasColumnType("datetime");

                entity.HasOne(d => d.Guest)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.GuestId)
                    .HasConstraintName("FK__Reservati__Guest__2F10007B");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK__Reservati__RoomI__300424B4");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasOne(d => d.RoomStatus)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.RoomStatusId)
                    .HasConstraintName("FK__Rooms__RoomStatu__2C3393D0");

                entity.HasOne(d => d.RoomType)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.RoomTypeId)
                    .HasConstraintName("FK__Rooms__RoomTypeI__2B3F6F97");
            });

            modelBuilder.Entity<RoomStatus>(entity =>
            {
                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("RoomStatus");
            });

            modelBuilder.Entity<RoomType>(entity =>
            {
                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("RoomType");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
