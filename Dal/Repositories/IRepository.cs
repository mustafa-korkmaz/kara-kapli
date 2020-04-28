using System.Collections.Generic;
using Dal.Entities;

namespace Dal.Repositories
{
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        //IQueryable<TEntity> RawSql(string sql);

        TEntity GetById(object id);
        IEnumerable<TEntity> GetAll();
        void Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(int id);
    }
}
