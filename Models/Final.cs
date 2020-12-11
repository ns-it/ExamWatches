using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamWatches.Models
{
    [Table("final")]
    public partial class Final
    {
        [Key]
        [Column("id")]
        public short Id { get; set; }

        [Column("date")]
        [StringLength(50)]
        public string Date { get; set; }

        [Column("period")]
        [StringLength(10)]
        public string Period { get; set; }

        [Column("roomName")]
        [StringLength(50)]
        public string Roomname { get; set; }
        [Column("RoomChief")]
        [StringLength(50)]
        public string RoomChief { get; set; }
        [Column("RoomSecretarie")]
        [StringLength(50)]
        public string RoomSecretarie { get; set; }

        [Column("RoomWatcher")]
        [StringLength(50)]
        public string RoomWatcher { get; set; }


    }
}
