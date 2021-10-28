using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Tim_Xe.Data.Repository.Entities;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Tim_Xe.Data.Repository
{
    public partial class TimXeDBContext : DbContext
    {
        public TimXeDBContext()
        {
        }

        public TimXeDBContext(DbContextOptions<TimXeDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<BookingDriver> BookingDrivers { get; set; }
        public virtual DbSet<Channel> Channels { get; set; }
        public virtual DbSet<ChannelType> ChannelTypes { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<PriceKm> PriceKms { get; set; }
        public virtual DbSet<PriceTime> PriceTimes { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<VehicleType> VehicleTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=timxedb.cbkygjlh5adw.us-east-2.rds.amazonaws.com;Database=TimXeDB;User Id=admin;Password=12345678");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("Booking");

                entity.HasIndex(e => e.IdCustomer);

                entity.HasIndex(e => e.IdGroup);

                entity.HasIndex(e => e.IdVehicleType);

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.NameCustomer).HasMaxLength(50);

                entity.Property(e => e.PhoneCustomer).HasMaxLength(15);

                entity.Property(e => e.StartAt).HasColumnType("datetime");

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.IdCustomer)
                    .HasConstraintName("FK_Booking_Customer");

                entity.HasOne(d => d.IdGroupNavigation)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.IdGroup)
                    .HasConstraintName("FK_Booking_Group");

                entity.HasOne(d => d.IdVehicleTypeNavigation)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.IdVehicleType)
                    .HasConstraintName("FK_Trip_VehicleType");
            });

            modelBuilder.Entity<BookingDriver>(entity =>
            {
                entity.HasKey(e => new { e.IdBooking, e.IdDriver });

                entity.ToTable("Booking_Driver");

                entity.HasIndex(e => e.IdDriver);

                entity.Property(e => e.Note).HasMaxLength(200);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.IdBookingNavigation)
                    .WithMany(p => p.BookingDrivers)
                    .HasForeignKey(d => d.IdBooking)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Booking_Driver_Booking");

                entity.HasOne(d => d.IdDriverNavigation)
                    .WithMany(p => p.BookingDrivers)
                    .HasForeignKey(d => d.IdDriver)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Booking_Driver_Driver");
            });

            modelBuilder.Entity<Channel>(entity =>
            {
                entity.ToTable("Channel");

                entity.HasIndex(e => e.IdChannelType);

                entity.HasIndex(e => e.IdGroup);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Url).HasMaxLength(100);

                entity.HasOne(d => d.IdChannelTypeNavigation)
                    .WithMany(p => p.Channels)
                    .HasForeignKey(d => d.IdChannelType)
                    .HasConstraintName("FK_Channel_ChannelType");

                entity.HasOne(d => d.IdGroupNavigation)
                    .WithMany(p => p.Channels)
                    .HasForeignKey(d => d.IdGroup)
                    .HasConstraintName("FK_Channel_Group");
            });

            modelBuilder.Entity<ChannelType>(entity =>
            {
                entity.ToTable("ChannelType");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.CityName).HasMaxLength(50);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.HasIndex(e => e.Email)
                    .HasName("unique_email2")
                    .IsUnique();

                entity.HasIndex(e => e.Phone)
                    .HasName("unique_phone2")
                    .IsUnique();

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Img)
                    .HasColumnName("img")
                    .HasColumnType("text");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password).HasColumnType("text");

                entity.Property(e => e.Phone).HasMaxLength(12);

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.ToTable("Driver");

                entity.HasIndex(e => e.CardId)
                    .HasName("unique_cardid1")
                    .IsUnique();

                entity.HasIndex(e => e.CreateById);

                entity.HasIndex(e => e.Email)
                    .HasName("unique_email1")
                    .IsUnique();

                entity.HasIndex(e => e.Phone)
                    .HasName("unique_phone1")
                    .IsUnique();

                entity.Property(e => e.Address).HasMaxLength(150);

                entity.Property(e => e.CardId)
                    .HasColumnName("CardID")
                    .HasMaxLength(12);

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.CreateById).HasColumnName("CreateByID");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Img)
                    .HasColumnName("img")
                    .HasColumnType("text");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Phone).HasMaxLength(12);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.CreateBy)
                    .WithMany(p => p.Drivers)
                    .HasForeignKey(d => d.CreateById)
                    .HasConstraintName("FK_Driver_Manager");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PostDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Feedbacks_Customer");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Feedbacks_Driver");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group");

                entity.HasIndex(e => e.IdCity);

                entity.HasIndex(e => e.IdManager);

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.IdCityNavigation)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.IdCity)
                    .HasConstraintName("FK_Group_City");

                entity.HasOne(d => d.IdManagerNavigation)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.IdManager)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Group_Manager");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location");

                entity.HasIndex(e => e.IdBooking);

                entity.Property(e => e.Address).HasMaxLength(150);

                entity.Property(e => e.LatLng).HasMaxLength(100);

                entity.HasOne(d => d.IdBookingNavigation)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.IdBooking)
                    .HasConstraintName("FK_Point_Trip");
            });

            modelBuilder.Entity<Manager>(entity =>
            {
                entity.ToTable("Manager");

                entity.HasIndex(e => e.CardId)
                    .HasName("unique_cardid")
                    .IsUnique();

                entity.HasIndex(e => e.Email)
                    .HasName("unique_email")
                    .IsUnique();

                entity.HasIndex(e => e.Phone)
                    .HasName("unique_phone")
                    .IsUnique();

                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.CardId)
                    .HasColumnName("CardID")
                    .HasMaxLength(12);

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Img)
                    .HasColumnName("img")
                    .HasColumnType("text");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password).HasColumnType("text");

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Managers)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Acount_Role");
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.HasIndex(e => e.IdGroup);

                entity.Property(e => e.Content).HasMaxLength(200);

                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.IdGroupNavigation)
                    .WithMany(p => p.News)
                    .HasForeignKey(d => d.IdGroup)
                    .HasConstraintName("FK_News_Group");
            });

            modelBuilder.Entity<PriceKm>(entity =>
            {
                entity.ToTable("PriceKm");

                entity.HasIndex(e => e.IdVehicleType);

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.HasOne(d => d.IdVehicleTypeNavigation)
                    .WithMany(p => p.PriceKms)
                    .HasForeignKey(d => d.IdVehicleType)
                    .HasConstraintName("FK_PriceKm_VehicleType");
            });

            modelBuilder.Entity<PriceTime>(entity =>
            {
                entity.ToTable("PriceTime");

                entity.HasIndex(e => e.IdVehicleType);

                entity.HasOne(d => d.IdVehicleTypeNavigation)
                    .WithMany(p => p.PriceTimes)
                    .HasForeignKey(d => d.IdVehicleType)
                    .HasConstraintName("FK_PriceTime_VehicleType");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.ToTable("Vehicle");

                entity.HasIndex(e => e.IdDriver);

                entity.HasIndex(e => e.IdVehicleType);

                entity.Property(e => e.LicensePlate).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.IdDriverNavigation)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.IdDriver)
                    .HasConstraintName("FK_Vehicle_Driver");

                entity.HasOne(d => d.IdVehicleTypeNavigation)
                    .WithMany(p => p.Vehicles)
                    .HasForeignKey(d => d.IdVehicleType)
                    .HasConstraintName("FK_Vehicle_VehicleType");
            });

            modelBuilder.Entity<VehicleType>(entity =>
            {
                entity.ToTable("VehicleType");

                entity.HasIndex(e => e.NameType)
                    .HasName("unique_nameTypes")
                    .IsUnique();

                entity.Property(e => e.NameType).HasMaxLength(50);

                entity.Property(e => e.Note).HasMaxLength(200);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
