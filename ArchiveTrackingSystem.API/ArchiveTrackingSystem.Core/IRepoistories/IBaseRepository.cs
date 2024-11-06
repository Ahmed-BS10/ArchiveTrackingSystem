using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveTrackingSystem.Core.IRepoistories
{
    public interface IBaseRepository<T> where T : class
    {

        Task<IEnumerable<T>> GetListAsync();
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
        IQueryable<T> GetTableNoTracking();
        IQueryable<T> GetTableAsTracking();

        Task<IEnumerable<T>> GetListWithincludesAsync(string[] includes = null);
        Task<T> Find(Expression<Func<T, bool>> predicate, string[] includes = null);


    }
}
