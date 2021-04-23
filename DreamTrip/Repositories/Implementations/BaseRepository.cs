using DreamTrip.Models;
using DreamTrip.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DreamTrip.Repositories.Implementations
{
    public class BaseRepository<T> : ITripApplicationRepository<T>
        where T : BaseModel, new()
    {
        protected readonly DreamTripDbContext dbContext;
        protected readonly DbSet<T> dbSet;

        public  BaseRepository(DreamTripDbContext context)
        {
            dbContext = context;
            dbSet = context.Set<T>();
        }
        public void Delete(int id)
        {
            dbSet.Remove(GetById(id));

            dbContext.SaveChanges();
        }

        public List<T> GetAll()
        {
            return dbSet.ToList();
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public void Insert(T item)
        {
            dbSet.Add(item);

            dbContext.SaveChanges();
        }

        public void Update(T item)
        {
            T element = GetById(item.Id);

            if (element != null)
            {
                dbContext.Entry(element).State = EntityState.Detached;
            }

            dbContext.Entry(item).State = EntityState.Modified;

            dbContext.SaveChanges();
        }
    }
}
