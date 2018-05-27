using System;
using System.Collections.Generic;

namespace CoreFaces.Identity.Repositories
{
    public interface IBaseRepository<TEntity>
    {
        TEntity GetById(Guid id);
        Guid Save(TEntity tEntity);
        bool Update(TEntity tEntity);
        bool Delete(Guid id);
        List<TEntity> GetAll();
    }
}
