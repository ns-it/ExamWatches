using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ExamWatches.Models
{
    public partial class exam_watchesContext : DbContext
    {
        public exam_watchesContext()
        {
        }

        public exam_watchesContext(DbContextOptions<exam_watchesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Exam> Exam { get; set; }
        public virtual DbSet<Period> Period { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Watch> Watch { get; set; }
        public virtual DbSet<Watcher> Watcher { get; set; }
        public virtual DbSet<WorkLocation> WorkLocation { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["WatchConn"].ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exam>(entity =>
            {
                entity.HasOne(d => d.WorkLocation)
                    .WithMany(p => p.Exam)
                    .HasForeignKey(d => d.WorkLocationId)
                    .HasConstraintName("FK_exam_work_location");
            });

            modelBuilder.Entity<Period>(entity =>
            {
                entity.HasOne(d => d.WorkLocation)
                    .WithMany(p => p.Period)
                    .HasForeignKey(d => d.WorkLocationId)
                    .HasConstraintName("FK_period_work_location");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasOne(d => d.WorkLocation)
                    .WithMany(p => p.Room)
                    .HasForeignKey(d => d.WorkLocationId)
                    .HasConstraintName("FK_room_work_location");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.WorkLocation)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.WorkLocationId)
                    .HasConstraintName("FK_users_work_location");
            });

            modelBuilder.Entity<Watch>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.RoomId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.Watch)
                    .HasForeignKey(d => d.ExamId)
                    .HasConstraintName("FK_watch_exam");

                entity.HasOne(d => d.Period)
                    .WithMany(p => p.Watch)
                    .HasForeignKey(d => d.PeriodId)
                    .HasConstraintName("FK_watch_period");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Watch)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_watch_room");

                entity.HasOne(d => d.Watcher)
                    .WithMany(p => p.Watch)
                    .HasForeignKey(d => d.WatcherId)
                    .HasConstraintName("FK_watch_watcher");
            });

            modelBuilder.Entity<Watcher>(entity =>
            {
                entity.HasOne(d => d.WorkLocation)
                    .WithMany(p => p.Watcher)
                    .HasForeignKey(d => d.WorkLocationId)
                    .HasConstraintName("FK_watcher_work_location");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
