using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamWatches.Models
{
    [Table("room")]
    public partial class Room
    {
        public Room()
        {
            WatcherWatches = new HashSet<WatcherWatch>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; }
        [Column("type")]
        public short Type { get; set; }
        [Column("capacity")]
        public short? Capacity { get; set; }
        [Column("work_location_id")]
        public short? WorkLocationId { get; set; }

        [ForeignKey(nameof(WorkLocationId))]
        [InverseProperty("Rooms")]
        public virtual WorkLocation WorkLocation { get; set; }
        [InverseProperty("Room")]
        public virtual ICollection<WatcherWatch> WatcherWatches { get; set; }
    }
}
