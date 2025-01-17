﻿// <auto-generated />
using System;
using Lab4.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lab04.Migrations
{
    [DbContext(typeof(SchoolCommunityContext))]
    [Migration("20211211182335_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Lab4.Models.Advertisement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CommunityId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CommunityId");

                    b.ToTable("Advertisement");
                });

            modelBuilder.Entity("Lab4.Models.Community", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Budget")
                        .HasColumnType("money");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Community");
                });

            modelBuilder.Entity("Lab4.Models.CommunityMembership", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<string>("CommunityId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("StudentId1")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("StudentId", "CommunityId");

                    b.HasIndex("CommunityId");

                    b.HasIndex("StudentId1");

                    b.ToTable("CommunityMembership");
                });

            modelBuilder.Entity("Lab4.Models.Student", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("EnrollmentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("Lab4.Models.Advertisement", b =>
                {
                    b.HasOne("Lab4.Models.Community", "Community")
                        .WithMany("Advertisements")
                        .HasForeignKey("CommunityId");

                    b.Navigation("Community");
                });

            modelBuilder.Entity("Lab4.Models.CommunityMembership", b =>
                {
                    b.HasOne("Lab4.Models.Community", null)
                        .WithMany("memberships")
                        .HasForeignKey("CommunityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Lab4.Models.Student", null)
                        .WithMany("memberships")
                        .HasForeignKey("StudentId1");
                });

            modelBuilder.Entity("Lab4.Models.Community", b =>
                {
                    b.Navigation("Advertisements");

                    b.Navigation("memberships");
                });

            modelBuilder.Entity("Lab4.Models.Student", b =>
                {
                    b.Navigation("memberships");
                });
#pragma warning restore 612, 618
        }
    }
}
