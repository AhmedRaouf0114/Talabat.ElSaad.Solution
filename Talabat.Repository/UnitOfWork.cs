using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _dbContext;

        private Dictionary<string,GenaricRepository<BaseEntity>> _Repositories { get; set; }

        public UnitOfWork(StoreContext dbContext)
        {
            _dbContext = dbContext;
            _Repositories = new Dictionary<string, GenaricRepository<BaseEntity>>();
        }

        public IGenaricRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            var Key = typeof(TEntity).Name;

            if (!_Repositories.ContainsKey(Key))
            {
                var Repo = new GenaricRepository<TEntity>(_dbContext);

                _Repositories.Add(Key, Repo as GenaricRepository<BaseEntity>);
            }
            return _Repositories[Key] as IGenaricRepository<TEntity>;
        }

        public async Task<int> CompleteAsunc()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _dbContext.DisposeAsync();
        }
    }
}
