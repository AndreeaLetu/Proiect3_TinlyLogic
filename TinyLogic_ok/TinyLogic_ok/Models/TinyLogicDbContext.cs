using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;
using System.Reflection.Emit;

namespace TinyLogic_ok.Models
{
    public class TinyLogicDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public TinyLogicDbContext(DbContextOptions<TinyLogicDbContext> options)
            : base(options)
        {
        }

        // DbSets
        public DbSet<User> Users { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Lessons> Lessons { get; set; }
        public DbSet<LessonQuiz> LessonQuiz { get; set; }
        public DbSet<Tests> Tests { get; set; }
        public DbSet<UserLessons> UserLessons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

          
            modelBuilder.Entity<Courses>()
                .HasMany(c => c.Lessons)
                .WithOne(l => l.Course)
                .HasForeignKey(l => l.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

        
            modelBuilder.Entity<Lessons>()
                .HasOne(l => l.Quiz)
                .WithOne(q => q.Lesson)
                .HasForeignKey<LessonQuiz>(q => q.LessonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Courses>()
                .HasOne(c => c.Test)
                .WithOne(t => t.Course)
                .HasForeignKey<Tests>(t => t.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

      
            modelBuilder.Entity<UserLessons>()
                .HasOne(ul => ul.User)
                .WithMany(u => u.LessonsProgress)
                .HasForeignKey(ul => ul.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        
            modelBuilder.Entity<UserLessons>()
                .HasOne(ul => ul.Lesson)
                .WithMany(l => l.UserProgress)
                .HasForeignKey(ul => ul.LessonId)
                .OnDelete(DeleteBehavior.Cascade);


            

            modelBuilder.Entity<Lessons>()
                .Property(l => l.LessonName)
                .IsRequired();
        }
    }
}
