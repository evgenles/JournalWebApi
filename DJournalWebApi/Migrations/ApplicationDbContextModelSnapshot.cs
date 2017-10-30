﻿// <auto-generated />

using System;
using DJournalWebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DJournalWebApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DJournalWebApi.Model.Cell", b =>
            {
                b.Property<Guid>("CellId")
                    .ValueGeneratedOnAdd();

                b.Property<string>("Comment");

                b.Property<Guid?>("SheetDatesId");

                b.Property<Guid?>("SheetId");

                b.Property<Guid?>("SheetStudentId");

                b.Property<bool?>("VisitState");

                b.HasKey("CellId");

                b.HasIndex("SheetDatesId");

                b.HasIndex("SheetId");

                b.HasIndex("SheetStudentId");

                b.ToTable("Cells");
            });

            modelBuilder.Entity("DJournalWebApi.Model.Group", b =>
            {
                b.Property<Guid>("GroupId")
                    .ValueGeneratedOnAdd();

                b.Property<string>("NewName");

                b.Property<string>("OldName");

                b.HasKey("GroupId");

                b.ToTable("Groups");
            });

            modelBuilder.Entity("DJournalWebApi.Model.GroupSheet", b =>
            {
                b.Property<Guid>("GroupSheetId")
                    .ValueGeneratedOnAdd();

                b.Property<Guid?>("GroupId");

                b.Property<Guid?>("SheetId");

                b.HasKey("GroupSheetId");

                b.HasIndex("GroupId");

                b.HasIndex("SheetId");

                b.ToTable("GroupSheets");
            });

            modelBuilder.Entity("DJournalWebApi.Model.Sheet", b =>
            {
                b.Property<Guid>("SheetId")
                    .ValueGeneratedOnAdd();

                b.Property<string>("Name");

                b.Property<Guid?>("SubjectId");

                b.Property<Guid?>("TeacherId");

                b.HasKey("SheetId");

                b.HasIndex("SubjectId");

                b.HasIndex("TeacherId");

                b.ToTable("Sheets");
            });

            modelBuilder.Entity("DJournalWebApi.Model.SheetDates", b =>
            {
                b.Property<Guid>("SheetDatesId")
                    .ValueGeneratedOnAdd();

                b.Property<DateTime>("Date");

                b.Property<Guid?>("SheetId");

                b.HasKey("SheetDatesId");

                b.HasIndex("SheetId");

                b.ToTable("SheetDates");
            });

            modelBuilder.Entity("DJournalWebApi.Model.SheetStudents", b =>
            {
                b.Property<Guid>("SheetStudentsId")
                    .ValueGeneratedOnAdd();

                b.Property<Guid?>("SheetId");

                b.Property<Guid?>("StudentId");

                b.HasKey("SheetStudentsId");

                b.HasIndex("SheetId");

                b.HasIndex("StudentId");

                b.ToTable("SheetStudents");
            });

            modelBuilder.Entity("DJournalWebApi.Model.Student", b =>
            {
                b.Property<Guid>("StudentId")
                    .ValueGeneratedOnAdd();

                b.Property<Guid?>("GroupId");

                b.Property<string>("Name");

                b.HasKey("StudentId");

                b.HasIndex("GroupId");

                b.ToTable("Students");
            });

            modelBuilder.Entity("DJournalWebApi.Model.Subject", b =>
            {
                b.Property<Guid>("SubjectId")
                    .ValueGeneratedOnAdd();

                b.Property<string>("Name");

                b.Property<string>("Type");

                b.HasKey("SubjectId");

                b.ToTable("Subjects");
            });

            modelBuilder.Entity("DJournalWebApi.Model.Teacher", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<int>("AccessFailedCount");

                b.Property<string>("ConcurrencyStamp")
                    .IsConcurrencyToken();

                b.Property<string>("Email")
                    .HasMaxLength(256);

                b.Property<bool>("EmailConfirmed");

                b.Property<string>("FullName");

                b.Property<bool>("LockoutEnabled");

                b.Property<DateTimeOffset?>("LockoutEnd");

                b.Property<string>("NormalizedEmail")
                    .HasMaxLength(256);

                b.Property<string>("NormalizedUserName")
                    .HasMaxLength(256);

                b.Property<string>("PasswordHash");

                b.Property<string>("PhoneNumber");

                b.Property<bool>("PhoneNumberConfirmed");

                b.Property<string>("SecurityStamp");

                b.Property<bool>("TwoFactorEnabled");

                b.Property<string>("UserName")
                    .HasMaxLength(256);

                b.HasKey("Id");

                b.HasIndex("NormalizedEmail")
                    .HasName("EmailIndex");

                b.HasIndex("NormalizedUserName")
                    .IsUnique()
                    .HasName("UserNameIndex")
                    .HasFilter("[NormalizedUserName] IS NOT NULL");

                b.ToTable("AspNetUsers");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
            {
                b.Property<Guid>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("ConcurrencyStamp")
                    .IsConcurrencyToken();

                b.Property<string>("Discriminator")
                    .IsRequired();

                b.Property<string>("Name")
                    .HasMaxLength(256);

                b.Property<string>("NormalizedName")
                    .HasMaxLength(256);

                b.HasKey("Id");

                b.HasIndex("NormalizedName")
                    .IsUnique()
                    .HasName("RoleNameIndex")
                    .HasFilter("[NormalizedName] IS NOT NULL");

                b.ToTable("AspNetRoles");

                b.HasDiscriminator<string>("Discriminator").HasValue("IdentityRole<Guid>");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("ClaimType");

                b.Property<string>("ClaimValue");

                b.Property<Guid>("RoleId");

                b.HasKey("Id");

                b.HasIndex("RoleId");

                b.ToTable("AspNetRoleClaims");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<string>("ClaimType");

                b.Property<string>("ClaimValue");

                b.Property<Guid>("UserId");

                b.HasKey("Id");

                b.HasIndex("UserId");

                b.ToTable("AspNetUserClaims");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
            {
                b.Property<string>("LoginProvider");

                b.Property<string>("ProviderKey");

                b.Property<string>("ProviderDisplayName");

                b.Property<Guid>("UserId");

                b.HasKey("LoginProvider", "ProviderKey");

                b.HasIndex("UserId");

                b.ToTable("AspNetUserLogins");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
            {
                b.Property<Guid>("UserId");

                b.Property<Guid>("RoleId");

                b.HasKey("UserId", "RoleId");

                b.HasIndex("RoleId");

                b.ToTable("AspNetUserRoles");
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
            {
                b.Property<Guid>("UserId");

                b.Property<string>("LoginProvider");

                b.Property<string>("Name");

                b.Property<string>("Value");

                b.HasKey("UserId", "LoginProvider", "Name");

                b.ToTable("AspNetUserTokens");
            });

            modelBuilder.Entity("DJournalWebApi.Model.Role", b =>
            {
                b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>");


                b.ToTable("Role");

                b.HasDiscriminator().HasValue("Role");
            });

            modelBuilder.Entity("DJournalWebApi.Model.Cell", b =>
            {
                b.HasOne("DJournalWebApi.Model.SheetDates", "SheetDates")
                    .WithMany("Cells")
                    .HasForeignKey("SheetDatesId")
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne("DJournalWebApi.Model.Sheet", "Sheet")
                    .WithMany("Cells")
                    .HasForeignKey("SheetId")
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne("DJournalWebApi.Model.SheetStudents", "SheetStudent")
                    .WithMany("Cells")
                    .HasForeignKey("SheetStudentId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("DJournalWebApi.Model.GroupSheet", b =>
            {
                b.HasOne("DJournalWebApi.Model.Group", "Group")
                    .WithMany()
                    .HasForeignKey("GroupId");

                b.HasOne("DJournalWebApi.Model.Sheet", "Sheet")
                    .WithMany("GroupSheets")
                    .HasForeignKey("SheetId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("DJournalWebApi.Model.Sheet", b =>
            {
                b.HasOne("DJournalWebApi.Model.Subject", "Subject")
                    .WithMany("Sheets")
                    .HasForeignKey("SubjectId")
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne("DJournalWebApi.Model.Teacher", "Teacher")
                    .WithMany("Sheets")
                    .HasForeignKey("TeacherId")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity("DJournalWebApi.Model.SheetDates", b =>
            {
                b.HasOne("DJournalWebApi.Model.Sheet", "Sheet")
                    .WithMany("SheetDates")
                    .HasForeignKey("SheetId")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity("DJournalWebApi.Model.SheetStudents", b =>
            {
                b.HasOne("DJournalWebApi.Model.Sheet", "Sheet")
                    .WithMany("SheetStudents")
                    .HasForeignKey("SheetId")
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasOne("DJournalWebApi.Model.Student", "Student")
                    .WithMany("SheetStudents")
                    .HasForeignKey("StudentId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("DJournalWebApi.Model.Student", b =>
            {
                b.HasOne("DJournalWebApi.Model.Group", "Group")
                    .WithMany("Students")
                    .HasForeignKey("GroupId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
            {
                b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>")
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
            {
                b.HasOne("DJournalWebApi.Model.Teacher")
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
            {
                b.HasOne("DJournalWebApi.Model.Teacher")
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
            {
                b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>")
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade);

                b.HasOne("DJournalWebApi.Model.Teacher")
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
            {
                b.HasOne("DJournalWebApi.Model.Teacher")
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade);
            });
#pragma warning restore 612, 618
        }
    }
}