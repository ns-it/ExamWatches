using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamWatches.Models
{
    [Table("watcher_watch")]
    public partial class WatcherWatch
    {
        [Key]
        [Column("watcher_id")]
        public long WatcherId { get; set; }
        [Key]
        [Column("watch_id")]
        public long WatchId { get; set; }
        [Column("room_id")]
        public int RoomId { get; set; }
        [Column("watcher_type")]
        [StringLength(50)]
        public string WatcherType { get; set; }
        [Column("attendence")]
        public bool Attendence { get; set; }

        [ForeignKey(nameof(RoomId))]
        [InverseProperty("WatcherWatches")]
        public virtual Room Room { get; set; }
        [ForeignKey(nameof(WatchId))]
        [InverseProperty("WatcherWatches")]
        public virtual Watch Watch { get; set; }
        [ForeignKey(nameof(WatcherId))]
        [InverseProperty("WatcherWatches")]
        public virtual Watcher Watcher { get; set; }
    }
}
