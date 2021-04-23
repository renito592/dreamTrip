using DreamTrip.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DreamTrip.ViewModels.TripApplication
{
    public class TripApplicationDetailsVM : BaseVM
    {
        public int UserId { get; set; }   

        public string ImageUrl { get; set; }
        
        public string UserFirstLastName { get; set; }

        public string UserEmail { get; set; }

        public int TripId { get; set; }

        public ApplicationStatus Status { get; set; }
    }
}
