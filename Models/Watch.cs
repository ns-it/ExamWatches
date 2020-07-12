using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamWatches.Models
{
    [Table("watch")]
    public partial class Watch
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("watcher_type")]
        [StringLength(50)]
        public string WatcherType { get; set; }
        [Column("attendence")]
        public bool Attendence { get; set; }
        [Column("watch_date", TypeName = "date")]
        public DateTime? WatchDate { get; set; }
        [Column("watcher_id")]
        public long? WatcherId { get; set; }
        [Column("period_id")]
        public short? PeriodId { get; set; }
        [Column("duration")]
        public short? Duration { get; set; }
        [Column("start_time", TypeName = "time(0)")]
        public TimeSpan? StartTime { get; set; }
        [Column("end_time", TypeName = "time(0)")]
        public TimeSpan? EndTime { get; set; }
        [Column("exam_id")]
        public short? ExamId { get; set; }
        [Column("room_id")]
        public int RoomId { get; set; }

        [ForeignKey(nameof(ExamId))]
        [InverseProperty("Watches")]
        public virtual Exam Exam { get; set; }
        [ForeignKey(nameof(PeriodId))]
        [InverseProperty("Watches")]
        public virtual Period Period { get; set; }
        [ForeignKey(nameof(RoomId))]
        [InverseProperty("Watches")]
        public virtual Room Room { get; set; }
        [ForeignKey(nameof(WatcherId))]
        [InverseProperty("Watches")]
        public virtual Watcher Watcher { get; set; }
    }
}
