using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DreamTrip.ViewModels.Auth
{
    public class LoginVM : BaseVM
    {
        [Required]
        [MaxLength(40)]
        [MinLength(3, ErrorMessage = "Minimum length of '3'")]
        public string Username { get; set; }

        [Required]
        [MaxLength(40)]
        [MinLength(6, ErrorMessage = "Minimum length of '6'")]
        public string Password { get; set; }
    }
}
