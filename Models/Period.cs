using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamWatches.Models
{
    [Table("period")]
    public partial class Period
    {
        public Period()
        {
            Watches = new HashSet<Watch>();
        }

        [Key]
        [Column("id")]
        public short Id { get; set; }
        [Column("duration")]
        public short? Duration { get; set; }
        [Column("start_time", TypeName = "time(0)")]
        public TimeSpan? StartTime { get; set; }
        [Column("end_time", TypeName = "time(0)")]
        public TimeSpan? EndTime { get; set; }
        [Column("work_location_id")]
        public short? WorkLocationId { get; set; }

        [ForeignKey(nameof(WorkLocationId))]
        [InverseProperty("Periods")]
        public virtual WorkLocation WorkLocation { get; set; }
        [InverseProperty("Period")]
        public virtual ICollection<Watch> Watches { get; set; }
    }
}
