using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiOpeqe.ViewModel
{
    public class Coordinates
    {
        [Required]
        public decimal latitude { get; set; }
        [Required]
        public decimal Longitude { get; set; }
    }
}
