using System.Collections.Generic;
using System.Linq;
using Dal.Entities;
using Dal.Db;
using Microsoft.EntityFrameworkCore;

namespace Dal.Repositories
{
    public abstract class PostgreSqlDbRepository<TEntity, TKey> : IRepository<TEntity> where TEntity : class, IEntity<TKey>
    {
        protected readonly DbSet<TEntity> Entities;
        private readonly BlackCoveredLedgerDbContext _context;

        public PostgreSqlDbRepository(BlackCoveredLedgerDbContext context)
        {
            _context = context;
            Entities = _context.Set<TEntity>();
        }

        public TEntity GetById(object id)
        {
            return Entities.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Entities.ToList();
        }

        public void Insert(TEntity entity)
        {
            Entities.Add(entity);
        }

        public void InsertRange(IEnumerable<TEntity> entities)
        {
            Entities.AddRange(entities);
        }

        public void Update(TEntity entity)
        {
            var attachedEntity = Entities.Local.FirstOrDefault(e => e.Id.Equals(entity.Id));

            if (attachedEntity != null)
            {
                _context.Entry(attachedEntity).State = EntityState.Detached;
            }

            //Entities.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            //check entity state
            var dbEntityEntry = _context.Entry(entity);

            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                Entities.Attach(entity);
                Entities.Remove(entity);
            }
        }

        /// <summary>
        /// sets and returns new entities which is different from main entity
        /// </summary>
        /// <typeparam name="TNewEntity"></typeparam>
        /// <returns></returns>
        protected DbSet<TNewEntity> GetEntities<TNewEntity>() where TNewEntity : EntityBase
        {
            return _context.Set<TNewEntity>();
        }
    }
}