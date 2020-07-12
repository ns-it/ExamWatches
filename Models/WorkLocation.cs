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
            Exam = new HashSet<Exam>();
            Period = new HashSet<Period>();
            Room = new HashSet<Room>();
            User = new HashSet<User>();
            Watcher = new HashSet<Watcher>();
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
        public virtual ICollection<Exam> Exam { get; set; }
        [InverseProperty("WorkLocation")]
        public virtual ICollection<Period> Period { get; set; }
        [InverseProperty("WorkLocation")]
        public virtual ICollection<Room> Room { get; set; }
        [InverseProperty("WorkLocation")]
        public virtual ICollection<User> User { get; set; }
        [InverseProperty("WorkLocation")]
        public virtual ICollection<Watcher> Watcher { get; set; }
    }
}
