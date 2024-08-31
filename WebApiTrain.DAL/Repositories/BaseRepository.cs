using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTrain.Domain.Interfaces.Repositories;

namespace WebApiTrain.DAL.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _dbcontext;

        public BaseRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public Task<TEntity> CreateAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("Entity is null");

            _dbcontext.Add(entity);
            _dbcontext.SaveChanges();

            return Task.FromResult(entity);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbcontext.Set<TEntity>();
        }

        public Task<TEntity> RemoveAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("Entity is null");

            _dbcontext.Remove(entity);
            _dbcontext.SaveChanges();

            return Task.FromResult(entity);
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null) throw new ArgumentNullException("Entity is null");

            _dbcontext.Update(entity);
            _dbcontext.SaveChanges();

            return Task.FromResult(entity);
        }
    }
}
