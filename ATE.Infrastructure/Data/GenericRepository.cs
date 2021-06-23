using System.Collections.Generic;
using System.Linq;
using ATE.Core.Entities;
using ATE.Core.Interfaces;
using ATE.Core.Specifications;

namespace ATE.Infrastructure.Data
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly AteContext _db;

        public GenericRepository(AteContext db)
        {
            _db = db;
        }

        public TEntity Add(TEntity item)
        {
            _db.Set<TEntity>().Add(item);
            _db.SaveChanges();

            return item;
        }

        public TEntity GetById(int id)
        {
            return _db.Set<TEntity>().Find(id);
        }

        public IReadOnlyList<TEntity> ListAll()
        {
            return _db.Set<TEntity>().ToList();
        }

        public void Remove(TEntity item)
        {
            _db.Set<TEntity>().Remove(item);
            _db.SaveChanges();
        }

        public TEntity GetEntityWithSpec(ISpecification<TEntity> spec)
        {
            return ApplySpec(spec).FirstOrDefault();
        }

        public IReadOnlyList<TEntity> List(ISpecification<TEntity> spec)
        {
            return ApplySpec(spec).ToList();
        }

        private IQueryable<TEntity> ApplySpec(ISpecification<TEntity> spec)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(_db.Set<TEntity>().AsQueryable(), spec);
        }
    }
}
