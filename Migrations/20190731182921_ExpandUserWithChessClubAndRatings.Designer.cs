﻿// <auto-generated />
using System;
using Isolani.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Isolani.Migrations
{
    [DbContext(typeof(IsolaniDbContext))]
    [Migration("20190731182921_ExpandUserWithChessClubAndRatings")]
    partial class ExpandUserWithChessClubAndRatings
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Isolani.Model.ChessClub", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("ChessClubs");
                });

            modelBuilder.Entity("Isolani.Model.Tournament", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("Isolani.Model.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BirthYear");

                    b.Property<int?>("BlitzRating");

                    b.Property<Guid?>("ChessClubId");

                    b.Property<string>("Country");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<DateTime>("LastLoginDate");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<int?>("RapidRating");

                    b.Property<int?>("StandardRating");

                    b.HasKey("Id");

                    b.HasIndex("ChessClubId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Isolani.Model.User", b =>
                {
                    b.HasOne("Isolani.Model.ChessClub", "ChessClub")
                        .WithMany()
                        .HasForeignKey("ChessClubId");
                });
#pragma warning restore 612, 618
        }
    }
}
