﻿using System.Collections.Generic;
using ATE.Core.Entities;
using ATE.Core.Specifications;

namespace ATE.Core.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        void Add(TEntity item);
        TEntity GetById(int id);
        IReadOnlyList<TEntity> ListAll();
        void Remove(TEntity item);
        TEntity GetEntityWithSpec(ISpecification<TEntity> spec);
        IReadOnlyList<TEntity> List(ISpecification<TEntity> spec);
    }
}
