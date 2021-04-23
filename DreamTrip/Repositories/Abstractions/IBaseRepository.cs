using DreamTrip.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DreamTrip.Repositories.Abstractions
{
    public interface ITripApplicationRepository<T> where T : BaseModel,new()
    {
        List<T> GetAll();
        T GetById(int id);
        void Insert(T item);
        void Update(T item);
        void Delete(int id);
    }
}
