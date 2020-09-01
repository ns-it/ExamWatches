using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamWatches.Models
{
    [Table("watcher")]
    public partial class Watcher
    {
        public Watcher()
        {
            Watches = new HashSet<Watch>();
           
        }

        [Key]
        [Column("id")]
        public long Id { get; set; }
        [Column("first_name")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Column("middle_name")]
        [StringLength(50)]
        public string MiddleName { get; set; }
        [Column("last_name")]
        [StringLength(50)]

        public string LastName { get; set; }
        [Column("full_name")]
        [StringLength(150)]
        public string FullName { get; }
        [Column("job")]
        [StringLength(50)]
        public string Job { get; set; }
        [Column("class")]
        public short? Class { get; set; }
        [Column("certificate")]
        [StringLength(50)]
        public string Certificate { get; set; }
        [Column("work_location_id")]
        public short? WorkLocationId { get; set; }


        [ForeignKey(nameof(WorkLocationId))]
        [InverseProperty("Watchers")]
        public virtual WorkLocation WorkLocation { get; set; }
        [InverseProperty("Watcher")]
        public virtual ICollection<Watch> Watches { get; set; }


   
  


    }
}
