using System;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ExamWatches.Models
{
    public partial class ExamWatchesDBContext : DbContext
    {
        public ExamWatchesDBContext()
        {
        }

        public ExamWatchesDBContext(DbContextOptions<ExamWatchesDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<Final> Finals { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Watch> Watches { get; set; }
        public virtual DbSet<Watcher> Watchers { get; set; }
        public virtual DbSet<WatcherWatch> WatcherWatches { get; set; }
        public virtual DbSet<WorkLocation> WorkLocations { get; set; }

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
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.WorkLocationId)
                    .HasConstraintName("FK_exam_work_location");
            });



            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasOne(d => d.WorkLocation)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.WorkLocationId)
                    .HasConstraintName("FK_room_work_location");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.FullName).HasComputedColumnSql("(concat([first_name],' ',[last_name]))");
                entity.HasOne(d => d.WorkLocation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.WorkLocationId)
                    .HasConstraintName("FK_users_work_location");
            });

            modelBuilder.Entity<Watch>(entity =>
            {
                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.Watches)
                    .HasForeignKey(d => d.ExamId)
                    .HasConstraintName("FK_watch_exam");

            });

            modelBuilder.Entity<Watcher>(entity =>
            {
                entity.Property(e => e.FullName).HasComputedColumnSql("(concat([title],' ',[first_name],' ',[middle_name],' ',[last_name]))");

                entity.HasOne(d => d.WorkLocation)
                    .WithMany(p => p.Watchers)
                    .HasForeignKey(d => d.WorkLocationId)
                    .HasConstraintName("FK_watcher_work_location");
            });

            modelBuilder.Entity<WatcherWatch>(entity =>
            {
                entity.HasKey(e => new { e.WatcherId, e.WatchId, e.RoomId });

                entity.HasOne(d => d.Room)
    .WithMany(p => p.WatcherWatches)
    .HasForeignKey(d => d.RoomId)
    .OnDelete(DeleteBehavior.ClientSetNull)
    .HasConstraintName("FK_watcher_watch_room");

                entity.HasOne(d => d.Watch)
                    .WithMany(p => p.WatcherWatches)
                    .HasForeignKey(d => d.WatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_watcher_watch_watch");

                entity.HasOne(d => d.Watcher)
                    .WithMany(p => p.WatcherWatches)
                    .HasForeignKey(d => d.WatcherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_watcher_watch_watcher");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
