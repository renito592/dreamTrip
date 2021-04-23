using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DreamTrip.Models
{
    public class User : BaseModel
    {
        [Required]
        [MaxLength(40)]
        public string Username { get; set; }

        [Required]
        [MaxLength(40)]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [MaxLength(40)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(40)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(40)]
        public string Email { get; set; }

        [Required]
        public bool Role { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<Trip> Trips { get; set; }

        public virtual ICollection<TripApplication> TripApplications { get; set; }

    }
}
