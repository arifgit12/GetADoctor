using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GetADoctor.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> All();
        IEnumerable<T> GetAll();
        T Find(object id);
        T Get(int id);
        void Add(T model);
        void Update(T model);
        void Remove(T model);      
        void Detach(T model);
        int SaveChanges();
        IQueryable<T> SearchFor(Expression<Func<T, bool>> conditions);
    }
}
