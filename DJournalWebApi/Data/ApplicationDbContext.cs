﻿using System;
using DJournalWebApi.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DJournalWebApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<Teacher, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Teacher> Teachers { get; set; }
#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
        public DbSet<Role> Roles { get; set; }
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
        public DbSet<Group> Groups { get; set; }
        public DbSet<Cell> Cells { get; set; }
        public DbSet<GroupSheet> GroupSheets { get; set; }
        public DbSet<Sheet> Sheets { get; set; }
        public DbSet<SheetDates> SheetDates { get; set; }
        public DbSet<SheetStudents> SheetStudents { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Teacher>()
                .HasMany(t => t.Sheets)
                .WithOne(s => s.Teacher)
                .HasForeignKey(s => s.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);



            builder.Entity<Student>()
                .HasMany(st => st.SheetStudents)
                .WithOne(s => s.Student)
                .HasForeignKey(s => s.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SheetStudents>()
                .HasMany(s => s.Cells)
                .WithOne(c => c.SheetStudent)
                .HasForeignKey(c => c.SheetStudentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SheetDates>()
                .HasMany(s => s.Cells)
                .WithOne(c => c.SheetDates)
                .HasForeignKey(c => c.SheetDatesId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Sheet>()
                .HasMany(s => s.SheetDates)
                .WithOne(c => c.Sheet)
                .HasForeignKey(c => c.SheetId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Sheet>()
                .HasMany(s => s.SheetStudents)
                .WithOne(c => c.Sheet)
                .HasForeignKey(c => c.SheetId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Sheet>()
                .HasMany(s => s.GroupSheets)
                .WithOne(c => c.Sheet)
                .HasForeignKey(c => c.SheetId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Group>()
                .HasMany(g => g.Students)
                .WithOne(s => s.Group)
                .HasForeignKey(s => s.GroupId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Group>()
                .HasMany(g => g.GroupSheets)
                .WithOne(gs => gs.Group)
                .HasForeignKey(gs => gs.GroupId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}