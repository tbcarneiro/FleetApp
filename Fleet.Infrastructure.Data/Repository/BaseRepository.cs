using Fleet.Domain.Entities;
using Fleet.Domain.Interfaces;
using Fleet.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fleet.Infrastructure.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        public SqliteContext context = new SqliteContext();

        public void Insert(T obj)
        {
            context.Database.EnsureCreated();
            context.Set<T>().Add(obj);
            context.SaveChanges();
        }

        public void Update(T obj)
        {
            context.Database.EnsureCreated();
            context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.Database.EnsureCreated();
            context.Set<T>().Remove(Select(id));
            context.SaveChanges();
        }

        public IList<T> Select()
        {
            context.Database.EnsureCreated();
            return context.Set<T>().ToList();
        }

        public T Select(int id)
        {
            context.Database.EnsureCreated();
            return context.Set<T>().Find(id);
        }

    }
}
