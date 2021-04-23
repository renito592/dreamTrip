using DreamTrip.Models;
using DreamTrip.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DreamTrip.Repositories.Implementations
{
    public class TripRepository : BaseRepository<Trip>, ITripRepository
    {
        public TripRepository(DreamTripDbContext context) : base(context)
        {
        }

        public List<Trip> GetAllWithUser()
        {
            return dbSet.Include(t => t.User).ToList();
        }

        public Trip GetById(int id, int userId)
        {
            return GetAll().FirstOrDefault(t => t.Id == id && t.UserId == userId);
        }

        public Trip GetByIdWithUser(int id)
        {
            return GetAllWithUser().FirstOrDefault(t => t.Id == id);
        }

    }
}
