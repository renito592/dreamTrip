using DreamTrip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DreamTrip.Repositories.Abstractions
{
    public interface ITripRepository : ITripApplicationRepository<Trip>
    {
        List<Trip> GetAllWithUser();
        Trip GetById(int id, int userId);
        Trip GetByIdWithUser(int id);
    }
}
