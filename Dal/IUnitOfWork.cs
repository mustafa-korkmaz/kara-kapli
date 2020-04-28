
using Dal.Entities;
using Dal.Repositories;
using System;

namespace Dal
{
    public interface IUnitOfWork : IDisposable
    {
        int Save();

        /// <summary>
        /// Returns entity repository inherited from  IRepository
        /// </summary>
        /// <typeparam name="TRepository"></typeparam>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        TRepository Repository<TRepository, TEntity>()
            where TEntity : EntityBase
            where TRepository : IRepository<TEntity>;

        /// <summary>
        /// Use only for entity object free repositories like IdentityUser's repository
        /// </summary>
        /// <typeparam name="TRepository"></typeparam>
        /// <returns></returns>
        TRepository Repository<TRepository>();
    }
}
