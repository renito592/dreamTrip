using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DreamTrip.Models
{
    public class Trip : BaseModel
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        
        public DateTime Date { get; set; }

        [Required]
        [Range(0,365)]
        public int Days { get; set; }

        [Required]
        [MaxLength(200)]
        public string Destinations { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        [MaxLength(100)]
        public string FromPlace { get; set; }

        [Required]
        [Range(0,1000)]
        public int Seats { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<TripApplication> TripApplications { get; set; }
    }
}
