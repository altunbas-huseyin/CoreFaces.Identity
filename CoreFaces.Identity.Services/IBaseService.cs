using System;
using System.Collections.Generic;

namespace CoreFaces.Identity.Services
{
    public interface IBaseService<TEntity>
    {
        TEntity GetById(Guid id);
        Guid Save(TEntity tEntity);
        bool Update(TEntity tEntity);
        bool Delete(Guid id);
        List<TEntity> GetAll();
    }
}
