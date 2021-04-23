using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DreamTrip.Models;

namespace DreamTrip.Repositories.Abstractions
{
    public interface IUserRepository : ITripApplicationRepository<User>
    {
        User GetByUsernameAndPassword(string username, string password);
        User GetByUsername(string username);
    }
}
