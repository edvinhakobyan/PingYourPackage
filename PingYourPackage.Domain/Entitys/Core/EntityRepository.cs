using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PingYourPackage.Domain
{
    class EntityRepository<T> : IEntityRepository<T> where T : class, IEntity, new()
    {
        readonly DbContext _dbContext;

        public EntityRepository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException("entitiesContext");
        }


        public void Add(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
        }

        public void Delete(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
        }

        public void Edit(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }


        public T GetSingle(Guid key)
        {
            return GetAll().FirstOrDefault(t => t.Key == key);
        }

        public IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>();
        }


        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public virtual IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProps)
        {
            IQueryable<T> query = GetAll();
            foreach (var includePropertie in includeProps)
            {
                query = query.Include(includePropertie);
            }
            return query;
        }

        public PaginatedList<T> Paginate<TKey>(int pageIndex, int pageSize, Expression<Func<T, TKey>> keySelector)
        {
            return Paginate(pageIndex, pageSize, keySelector, null);
        }

        public PaginatedList<T> Paginate<TKey>(int pageIndex, int pageSize, Expression<Func<T, TKey>> keySelector, Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProps)
        {
            IQueryable<T> query = AllIncluding(includeProps).OrderBy(keySelector);
            query = (predicate == null) ? query : query.Where(predicate);
            return query.ToPaginatedList(pageIndex, pageSize);
        }

        public IQueryable<T> All => GetAll();
    }
}
