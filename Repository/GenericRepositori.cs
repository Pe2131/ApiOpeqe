using DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class GenericRepositori<TEntity> where TEntity : class
    {

        private ApplicationDbContext _context;
        private DbSet<TEntity> _dbset;

        public GenericRepositori(ApplicationDbContext context)
        {
            _context = context;
            _dbset = context.Set<TEntity>();
        }


        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> where = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderby = null, string includes = "")
        {
            IQueryable<TEntity> query = _dbset;

            if (where != null)
            {
                query = query.Where(where);
            }

            if (orderby != null)
            {
                query = orderby(query);
            }

            if (includes != "")
            {
                foreach (string include in includes.Split(','))
                {
                    query = query.Include(include);
                }
            }

            return query.ToList();
        }

        public virtual TEntity GetById(object id)
        {
            return _dbset.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            _dbset.Add(entity);
        }
        public virtual void InsertRange(List<TEntity> entity)
        {
            _dbset.AddRange(entity);
        }
        public virtual void Update(TEntity entity)
        {
            _dbset.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
        public virtual void UpdateRange(IEnumerable<TEntity> entity)
        {
            _dbset.AttachRange(entity);
        }
        public virtual void UpdateWithOption(TEntity entity, string property)
        {
            _dbset.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.Entry(entity).Property(property).IsModified = false;
        }
        public virtual void Delete(TEntity entity)
        {
            _dbset.Remove(entity);
        }
        public virtual void Delete(IEnumerable<TEntity> entity)
        {
            _dbset.RemoveRange(entity);
        }

        public virtual void Delete(object id)
        {
            var entity = GetById(id);
            Delete(entity);
        }
        public virtual void Detach(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
