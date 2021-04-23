using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DreamTrip.ViewModels.Trip
{
    public class TripEditVM : BaseVM
    {
        [Required]
        [MaxLength(100)]
        [MinLength(3, ErrorMessage = "Minimum length of '3'")]
        public string Title { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Range(0, 365)]
        public int Days { get; set; }

        [Required]
        [MaxLength(200)]
        [MinLength(3, ErrorMessage = "Minimum length of '3'")]
        public string Destinations { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(3, ErrorMessage = "Minimum length of '3'")]
        public string FromPlace { get; set; }
       
        [Required]
        public double Price { get; set; }

        [Required]
        [Range(0, 1000)]
        public int Seats { get; set; }

        [DataType(DataType.ImageUrl,ErrorMessage ="Not valid Image Url")]
        public string ImageUrl { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Minimum length of '3'")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
