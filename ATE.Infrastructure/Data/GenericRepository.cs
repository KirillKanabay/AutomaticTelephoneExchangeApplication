using System;
using System.Collections.Generic;
using ATE.Core.Entities;
using ATE.Core.Interfaces;

namespace ATE.Infrastructure.Data
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {

        public TEntity Add(TEntity item)
        {
            throw new NotImplementedException();
        }

        public TEntity GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<TEntity> ListAll()
        {
            throw new NotImplementedException();
        }

        public TEntity Delete(TEntity item)
        {
            throw new NotImplementedException();
        }

        public TEntity GetEntityWithSpec()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyList<TEntity> List()
        {
            throw new NotImplementedException();
        }
    }
}
