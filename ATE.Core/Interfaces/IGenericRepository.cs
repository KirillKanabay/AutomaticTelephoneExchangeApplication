using System.Collections.Generic;
using ATE.Core.Entities;

namespace ATE.Core.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity Add(TEntity item);
        TEntity GetById(int id);
        IReadOnlyList<TEntity> ListAll();
        TEntity Delete(TEntity item);
        TEntity GetEntityWithSpec();
        IReadOnlyList<TEntity> List();
    }
}
