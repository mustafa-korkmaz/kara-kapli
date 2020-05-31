using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Dal.Entities;
using Dal.Repositories;
using Dal.Db;

namespace Dal
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlackCoveredLedgerDbContext _context;
        private bool _disposed;
        private Dictionary<string, object> _repositories;

        public UnitOfWork(BlackCoveredLedgerDbContext context)
        {
            _context = context;
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public TRepository Repository<TRepository, TEntity>()
      where TEntity : class
      where TRepository : IRepository<TEntity>
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object>();
            }

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryInterfaceType = typeof(TRepository);

                var assignedTypesToRepositoryInterface = Assembly.GetExecutingAssembly().GetTypes().Where(t => repositoryInterfaceType.IsAssignableFrom(t)); //all types of your plugin

                var repositoryType = assignedTypesToRepositoryInterface.First(p => p.Name[0] != 'I'); //filter interfaces, select only first implemented class

                var repositoryInstance = Activator.CreateInstance(repositoryType, _context);
                _repositories.Add(type, repositoryInstance);
            }

            return (TRepository)_repositories[type];
        }

        public TRepository Repository<TRepository>()
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string, object>();
            }

            var type = "EntityFree_" + typeof(TRepository).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryInterfaceType = typeof(TRepository);

                var assignedTypesToRepositoryInterface = Assembly.GetExecutingAssembly().GetTypes().Where(t => repositoryInterfaceType.IsAssignableFrom(t)); //all types of your plugin

                var repositoryType = assignedTypesToRepositoryInterface.First(p => p.Name[0] != 'I'); //filter interfaces, select only first implemented class

                var repositoryInstance = Activator.CreateInstance(repositoryType, _context);
                _repositories.Add(type, repositoryInstance);
            }

            return (TRepository)_repositories[type];
        }
    }
}
