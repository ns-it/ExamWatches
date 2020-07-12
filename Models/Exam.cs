using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamWatches.Models
{
    [Table("exam")]
    public partial class Exam
    {
        public Exam()
        {
            Watches = new HashSet<Watch>();
        }

        [Key]
        [Column("id")]
        public short Id { get; set; }
        [Column("academic_year")]
        [StringLength(10)]
        public string AcademicYear { get; set; }
        [Column("semester")]
        public short? Semester { get; set; }
        [Column("days_number")]
        public short? DaysNumber { get; set; }
        [Column("start_time", TypeName = "date")]
        public DateTime? StartTime { get; set; }
        [Column("end_time", TypeName = "date")]
        public DateTime? EndTime { get; set; }
        [Column("work_location_id")]
        public short? WorkLocationId { get; set; }

        [ForeignKey(nameof(WorkLocationId))]
        [InverseProperty("Exams")]
        public virtual WorkLocation WorkLocation { get; set; }
        [InverseProperty("Exam")]
        public virtual ICollection<Watch> Watches { get; set; }
    }
}
