﻿
using Hotel.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace EntityFrameworkProgect.Migrations
{
    [DbContext(typeof(HotelDatabaseContext))]
    partial class HotelDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {

            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Hotel.Shared.Models.Guest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("City")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Country")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Phone")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("ReservationsCount")
                        .IsUnicode(false)
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Phone" }, "UQ__Guests__5C7E359E6BCB5E46")
                        .IsUnique()
                        .HasFilter("[Phone] IS NOT NULL");

                    b.HasIndex(new[] { "Email" }, "UQ__Guests__A9D10534785B8791")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.ToTable("Guests");
                });

            modelBuilder.Entity("Hotel.Shared.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("GuestId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("PayTime")
                        .HasColumnType("datetime");

                    b.Property<int?>("ReservationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GuestId");

                    b.HasIndex("ReservationId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Hotel.Shared.Models.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime?>("CheckInDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("CheckOutDate")
                        .HasColumnType("datetime");

                    b.Property<int?>("GuestId")
                        .HasColumnType("int");

                    b.Property<int?>("PersonCount")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ReservationDate")
                        .HasColumnType("datetime");

                    b.Property<int?>("RoomId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GuestId");

                    b.HasIndex("RoomId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("Hotel.Shared.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("MaxPerson")
                        .HasColumnType("int");

                    b.Property<int>("RoomNumber")
                        .HasColumnType("int");

                    b.Property<int?>("RoomStatusId")
                        .HasColumnType("int");

                    b.Property<int?>("RoomTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoomStatusId");

                    b.HasIndex("RoomTypeId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("Hotel.Shared.Models.RoomStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("RoomStatus");

                    b.HasKey("Id");

                    b.ToTable("RoomStatuses");
                });

            modelBuilder.Entity("Hotel.Shared.Models.RoomType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("RoomType");

                    b.HasKey("Id");

                    b.ToTable("RoomTypes");
                });

            modelBuilder.Entity("Hotel.Shared.Models.Payment", b =>
                {
                    b.HasOne("Hotel.Shared.Models.Guest", "Guest")
                        .WithMany("Payments")
                        .HasForeignKey("GuestId")
                        .HasConstraintName("FK__Payments__GuestI__32E0915F");

                    b.HasOne("Hotel.Shared.Models.Reservation", "Reservation")
                        .WithMany("Payments")
                        .HasForeignKey("ReservationId")
                        .HasConstraintName("FK__Payments__Reserv__33D4B598");

                    b.Navigation("Guest");

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("Hotel.Shared.Models.Reservation", b =>
                {
                    b.HasOne("Hotel.Shared.Models.Guest", "Guest")
                        .WithMany("Reservations")
                        .HasForeignKey("GuestId")
                        .HasConstraintName("FK__Reservati__Guest__2F10007B");

                    b.HasOne("Hotel.Shared.Models.Room", "Room")
                        .WithMany("Reservations")
                        .HasForeignKey("RoomId")
                        .HasConstraintName("FK__Reservati__RoomI__300424B4");

                    b.Navigation("Guest");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("Hotel.Shared.Models.Room", b =>
                {
                    b.HasOne("Hotel.Shared.Models.RoomStatus", "RoomStatus")
                        .WithMany("Rooms")
                        .HasForeignKey("RoomStatusId")
                        .HasConstraintName("FK__Rooms__RoomStatu__2C3393D0");

                    b.HasOne("Hotel.Shared.Models.RoomType", "RoomType")
                        .WithMany("Rooms")
                        .HasForeignKey("RoomTypeId")
                        .HasConstraintName("FK__Rooms__RoomTypeI__2B3F6F97");

                    b.Navigation("RoomStatus");

                    b.Navigation("RoomType");
                });

            modelBuilder.Entity("Hotel.Shared.Models.Guest", b =>
                {
                    b.Navigation("Payments");

                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("Hotel.Shared.Models.Reservation", b =>
                {
                    b.Navigation("Payments");
                });

            modelBuilder.Entity("Hotel.Shared.Models.Room", b =>
                {
                    b.Navigation("Reservations");
                });

            modelBuilder.Entity("Hotel.Shared.Models.RoomStatus", b =>
                {
                    b.Navigation("Rooms");
                });

            modelBuilder.Entity("Hotel.Shared.Models.RoomType", b =>
                {
                    b.Navigation("Rooms");
                });
#pragma warning restore 612, 618
        }
    }
}
