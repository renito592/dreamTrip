using DreamTrip.Models;
using DreamTrip.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DreamTrip.Repositories.Implementations
{
    public class TripApplicationRepository : BaseRepository<TripApplication>, ITripApplicationRepository
    {
        public TripApplicationRepository(DreamTripDbContext context) : base(context)
        {
        }

        public TripApplication GetByTripIdUserId(int tripId, int userId)
        {
            return GetAll().FirstOrDefault(ta => ta.TripId == tripId && ta.UserId == userId);
        }

        public List<TripApplication> GetByTripIdWithUser(int tripId)
        {
            return dbSet.Include(ta => ta.User).Where(ta => ta.TripId == tripId).ToList();
        }
    }
}
