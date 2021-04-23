using DreamTrip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DreamTrip.Repositories.Abstractions
{
    public interface ITripApplicationRepository : ITripApplicationRepository<TripApplication>
    {
        List<TripApplication> GetByTripIdWithUser(int tripId);
        TripApplication GetByTripIdUserId(int tripId, int userId);
    }
}
