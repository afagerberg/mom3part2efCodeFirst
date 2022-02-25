﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using dt191gMom3Part2.Data;

#nullable disable

namespace dt191gMom3Part2.Migrations
{
    [DbContext(typeof(CollectionContext))]
    partial class CollectionContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.2");

            modelBuilder.Entity("dt191gMom3Part2.Models.Artist", b =>
                {
                    b.Property<int?>("ArtistId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ArtistName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ArtistId");

                    b.ToTable("Artist");
                });

            modelBuilder.Entity("dt191gMom3Part2.Models.CDalbum", b =>
                {
                    b.Property<int>("AlbumId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ArtistId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CdName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ReleaseYear")
                        .HasColumnType("INTEGER");

                    b.HasKey("AlbumId");

                    b.HasIndex("ArtistId");

                    b.ToTable("CDalbum");
                });

            modelBuilder.Entity("dt191gMom3Part2.Models.Lending", b =>
                {
                    b.Property<int>("LendingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AlbumId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LendingDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("LendingId");

                    b.HasIndex("AlbumId")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("Lending");
                });

            modelBuilder.Entity("dt191gMom3Part2.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("dt191gMom3Part2.Models.CDalbum", b =>
                {
                    b.HasOne("dt191gMom3Part2.Models.Artist", "Artist")
                        .WithMany("CDalbums")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artist");
                });

            modelBuilder.Entity("dt191gMom3Part2.Models.Lending", b =>
                {
                    b.HasOne("dt191gMom3Part2.Models.CDalbum", "CDalbum")
                        .WithOne("Lending")
                        .HasForeignKey("dt191gMom3Part2.Models.Lending", "AlbumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("dt191gMom3Part2.Models.User", "User")
                        .WithMany("Loans")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CDalbum");

                    b.Navigation("User");
                });

            modelBuilder.Entity("dt191gMom3Part2.Models.Artist", b =>
                {
                    b.Navigation("CDalbums");
                });

            modelBuilder.Entity("dt191gMom3Part2.Models.CDalbum", b =>
                {
                    b.Navigation("Lending");
                });

            modelBuilder.Entity("dt191gMom3Part2.Models.User", b =>
                {
                    b.Navigation("Loans");
                });
#pragma warning restore 612, 618
        }
    }
}
