using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Models
{
   public class History
    {
        [Key]
        public int id { get; set; }
        [Required]
        public decimal latitude { get; set; }
        [Required]
        public decimal Longitude { get; set; }
        public decimal distance { get; set; }
        public virtual IdentityUser User { get; set; }

    }
}
