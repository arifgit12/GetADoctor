using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;

namespace GetADoctor.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbContext context;
        private IDbSet<T> set;
        public Repository(DbContext context)
        {
            this.context = context;
            this.set = context.Set<T>();
        }

        public void Add(T model)
        {
            this.ChangeState(model, EntityState.Added);
        }

        public IQueryable<T> All()
        {
            return this.set;
        }

        public void Detach(T model)
        {
            var entry = this.context.Entry(model);
            entry.State = EntityState.Detached;
        }

        public T Find(object id)
        {
            return this.set.Find(id);
        }

        public T Get(int id)
        {
            return this.set.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return this.set.ToList();
        }

        public void Remove(T model)
        {
            this.ChangeState(model, EntityState.Deleted);
        }

        public int SaveChanges()
        {
            try
            {
                return this.context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                        int i = 5;
                    }
                }
                throw e;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> conditions)
        {
            return this.All().AsQueryable().Where(conditions);
        }

        public void Update(T model)
        {
            this.ChangeState(model, EntityState.Modified);
        }

        private void ChangeState(T entity, EntityState state)
        {
            var entry = this.context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.set.Attach(entity);
            }
            entry.State = state;
        }
    }
}
