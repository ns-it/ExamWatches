using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamWatches.Models
{
    [Table("watch")]
    public partial class Watch
    {
        public Watch()
        {
            WatcherWatches = new HashSet<WatcherWatch>();
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("exam_id")]
        public short? ExamId { get; set; }
        [Column("period_id")]
        public short? PeriodId { get; set; }
        [Column("room_id")]
        public int RoomId { get; set; }
        [Column("watch_date", TypeName = "date")]
        public DateTime? WatchDate { get; set; }
        [Column("duration", TypeName = "decimal(3, 0)")]
        public decimal? Duration { get; set; }
        [Column("start_time", TypeName = "time(0)")]
        public TimeSpan? StartTime { get; set; }
        [Column("work_location_id")]
        public short? WorkLocationId { get; set; }

        [ForeignKey(nameof(ExamId))]
        [InverseProperty("Watches")]
        public virtual Exam Exam { get; set; }
        [ForeignKey(nameof(RoomId))]
        [InverseProperty("Watches")]
        public virtual Room Room { get; set; }
        [InverseProperty("Watch")]
        public virtual ICollection<WatcherWatch> WatcherWatches { get; set; }
    }
}
