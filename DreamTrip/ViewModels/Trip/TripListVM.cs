using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DreamTrip.ViewModels.Trip
{
    public class TripListVM : BaseVM
    {

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public DateTime Date { get; set; }

        public int Days { get; set; }
       
        public string Destinations { get; set; }

        public double Price { get; set; }

        public int UserId { get; set; }

        public string UserFirstLastNames { get; set; }

    }
}
