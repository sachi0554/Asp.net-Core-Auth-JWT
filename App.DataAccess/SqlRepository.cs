using App.Core;
using App.Core.Abstract;
using App.Core.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DataAccess
{
    public class SqlRepository<T> : IRepository<T> where T : BaseEntity
    {
        internal ApplicationContext context;
        internal DbSet<T> dbSet;

        public SqlRepository(ApplicationContext Context)
        {
            this.context = Context;
            this.dbSet = context.Set<T>();
        }

        public IQueryable<T> Collection()
        {
            return dbSet;
        }

        public async Task Commit()
        {
           await  context.SaveChangesAsync();
        }

        public void Delete(string Id)
        {
            var t = Find(Id);
            if (context.Entry(t).State == EntityState.Detached)
                dbSet.Attach(t);

            dbSet.Remove(t);
        }

        public void Update(T t)
        {
            dbSet.Attach(t);
            context.Entry(t).State = EntityState.Modified;
        }

        public T Find(string Id)
        {
            return dbSet.Find(Id);
        }

        public void Insert(T t)
        {
            dbSet.Add(t);
        }


    }
}
