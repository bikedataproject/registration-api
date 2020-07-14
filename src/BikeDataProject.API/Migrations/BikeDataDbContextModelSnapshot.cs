﻿// <auto-generated />
using System;
using BikeDataProject.API.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BikeDataProject.API.Migrations
{
    [DbContext(typeof(BikeDataDbContext))]
    partial class BikeDataDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("BikeDataProject.API.Domain.Contribution", b =>
                {
                    b.Property<int>("ContributionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Distance")
                        .HasColumnType("integer");

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.Property<byte[]>("PointsGeom")
                        .HasColumnType("bytea");

                    b.Property<DateTime[]>("PointsTime")
                        .HasColumnType("timestamp without time zone[]");

                    b.Property<DateTime>("TimeStampStart")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("TimeStampStop")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UserAgent")
                        .HasColumnType("text");

                    b.HasKey("ContributionId");

                    b.ToTable("Contributions");
                });

            modelBuilder.Entity("BikeDataProject.API.Domain.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("AccessToken")
                        .HasColumnType("text");

                    b.Property<int>("ExpiresAt")
                        .HasColumnType("integer");

                    b.Property<int>("ExpiresIn")
                        .HasColumnType("integer");

                    b.Property<string>("Provider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderUser")
                        .HasColumnType("text");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime>("TokenCreationDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BikeDataProject.API.Domain.UserContribution", b =>
                {
                    b.Property<int>("UserContributionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("ContributionId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("UserContributionId");

                    b.HasIndex("UserId");

                    b.ToTable("UserContributions");
                });

            modelBuilder.Entity("BikeDataProject.API.Domain.UserContribution", b =>
                {
                    b.HasOne("BikeDataProject.API.Domain.User", null)
                        .WithMany("UserContributions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
