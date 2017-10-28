using DJournalWebApi.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace DJournalWebApi.Date
{
    public class ApplicationDbContext : IdentityDbContext<Teacher, IdentityRole<Guid>, Guid>
    {
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Cell> Cells { get; set; }
        public DbSet<GroupSheet> GroupSheets { get; set; }
        public DbSet<Sheet> Sheets { get; set; }
        public DbSet<SheetDates> SheetDates { get; set; }
        public DbSet<SheetStudents> SheetStudents { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Teacher>()
                .HasMany((t) => t.Sheets)
                .WithOne((s) => s.Teacher)
                .HasForeignKey((s) => s.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Subject>()
                    .HasMany((su) => su.Sheets)
                    .WithOne((sh) => sh.Subject)
                    .HasForeignKey((sh) => sh.SubjectId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Student>()
                    .HasMany((st) => st.SheetStudents)
                    .WithOne((s) => s.Student)
                    .HasForeignKey((s) => s.StudentId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SheetStudents>()
                    .HasMany((s) => s.Cells)
                    .WithOne((c) => c.SheetStudent)
                    .HasForeignKey((c) => c.SheetStudentId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SheetDates>()
                    .HasMany((s) => s.Cells)
                    .WithOne((c) => c.SheetDates)
                    .HasForeignKey((c) => c.SheetDatesId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Sheet>()
                    .HasMany((s) => s.Cells)
                    .WithOne((c) => c.Sheet)
                    .HasForeignKey((c) => c.SheetId)
                    .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Sheet>()
                    .HasMany((s) => s.SheetDates)
                    .WithOne((c) => c.Sheet)
                    .HasForeignKey((c) => c.SheetId)
                    .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Sheet>()
                    .HasMany((s) => s.SheetStudents)
                    .WithOne((c) => c.Sheet)
                    .HasForeignKey((c) => c.SheetId)
                    .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Sheet>()
                    .HasMany((s) => s.GroupSheets)
                    .WithOne((c) => c.Sheet)
                    .HasForeignKey((c) => c.SheetId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Group>()
                    .HasMany((g) => g.Students)
                    .WithOne((s) => s.Group)
                    .HasForeignKey((s) => s.GroupId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
