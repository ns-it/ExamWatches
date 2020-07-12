using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamWatches.Models
{
    [Table("user")]
    public partial class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("username")]
        [StringLength(30)]
        public string Username { get; set; }
        [Column("password")]
        [StringLength(50)]
        public string Password { get; set; }
        [Column("first_name")]
        [StringLength(30)]
        public string FirstName { get; set; }
        [Column("last_name")]
        [StringLength(30)]
        public string LastName { get; set; }
        [Column("work_location_id")]
        public short? WorkLocationId { get; set; }

        [ForeignKey(nameof(WorkLocationId))]
        [InverseProperty("Users")]
        public virtual WorkLocation WorkLocation { get; set; }
    }
}
