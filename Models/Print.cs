using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ExamWatches.Models
{
    [Table("print")]
    public partial class Print
    {
     
         
        
            [Key]
            [Column("id")]
            public short Id { get; set; }

            [Column("FullName")]
            [StringLength(50)]
            public string FullName { get; set; }

            [Column("worklocation")]
            [StringLength(50)]
            public string worklocation { get; set; }

            [Column("WatchDate")]
            [StringLength(50)]
            public string WatchDate { get; set; }
            [Column("PeriodId")]
            [StringLength(50)]
            public string PeriodId { get; set; }
            [Column("Name")]
            [StringLength(50)]
            public string Name { get; set; }

            [Column("WatchType")]
            [StringLength(50)]
            public string WatchType { get; set; }

        [Column("RoomChief")]
        [StringLength(50)]
        public string RoomChief { get; set; }

        [Column("attendence")]
       
        public bool attendence { get; set; }



    }
    }
