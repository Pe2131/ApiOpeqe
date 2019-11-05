using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiOpeqe.ViewModel
{
    public class SignUp
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        [Compare("password")]
        public string confPass { get; set; }


    }
}
