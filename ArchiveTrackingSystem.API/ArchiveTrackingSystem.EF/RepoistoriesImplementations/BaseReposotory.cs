using ArchiveTrackingSystem.Core.IRepoistories;
using ArchiveTrackingSystem.EF.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveTrackingSystem.EF.RepoistoriesImplementations
{
    public class BaseReposotory<T> : IBaseRepository<T> where T : class
    {
        private readonly ArchiveTrackingDbContext _dbContext;

        public BaseReposotory(ArchiveTrackingDbContext dbContext)
        {
            _dbContext=dbContext;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;

        }

        public async Task<T> DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            IQueryable<T> values = _dbContext.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    values = values.Include(include);
                }
            }
            return await values.Where(predicate).ToListAsync();
        }

        public async Task<T> Find(Expression<Func<T, bool>> predicate, string[] includes = null)
        {


            IQueryable<T> values = _dbContext.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    values = values.Include(include);
                }
            }
            return await values.FirstOrDefaultAsync(predicate);
        }
        public async Task<IEnumerable<T>> GetListAsync()
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetListWithincludesAsync(string[] includes = null)
        {
            IQueryable<T> values = _dbContext.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    values = values.Include(include);
                }
            }
            return await values.ToListAsync();
        }

        public IQueryable<T> GetTableAsTracking()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public IQueryable<T> GetTableNoTracking()
        {
            return _dbContext.Set<T>().AsNoTracking().AsQueryable();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }


    }
}
