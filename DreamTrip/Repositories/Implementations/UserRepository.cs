using DreamTrip.Models;
using DreamTrip.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DreamTrip.Repositories.Implementations
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DreamTripDbContext context) : base(context)
        {

        }

        public User GetByUsername(string username)
        {
            return GetAll().FirstOrDefault(user => user.Username == username);
        }

        public User GetByUsernameAndPassword(string username, string password)
        {
            return GetAll().FirstOrDefault(user => user.Username == username && user.Password == password);
        }
    }
}
