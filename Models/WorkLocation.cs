using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamWatches.Models
{
    [Table("work_location")]
    public partial class WorkLocation
    {
        public WorkLocation()
        {
            Exams = new HashSet<Exam>();
            Periods = new HashSet<Period>();
            Rooms = new HashSet<Room>();
            Users = new HashSet<User>();
            Watchers = new HashSet<Watcher>();
        }

        [Key]
        [Column("id")]
        public short Id { get; set; }
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; }
        [Column("dean")]
        [StringLength(50)]
        public string Dean { get; set; }
        [Column("manager")]
        [StringLength(50)]
        public string Manager { get; set; }

        [InverseProperty("WorkLocation")]
        public virtual ICollection<Exam> Exams { get; set; }
        [InverseProperty("WorkLocation")]
        public virtual ICollection<Period> Periods { get; set; }
        [InverseProperty("WorkLocation")]
        public virtual ICollection<Room> Rooms { get; set; }
        [InverseProperty("WorkLocation")]
        public virtual ICollection<User> Users { get; set; }
        [InverseProperty("WorkLocation")]
        public virtual ICollection<Watcher> Watchers { get; set; }
    }
}
