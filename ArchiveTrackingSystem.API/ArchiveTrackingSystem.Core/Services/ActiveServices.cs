using ArchiveTrackingSystem.Core.Entities;
using ArchiveTrackingSystem.Core.IRepoistories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveTrackingSystem.Core.Services
{
    public class ActiveServices
    {
        private readonly IBaseRepository<Active> _activeRepository;

        public ActiveServices(IBaseRepository<Active> activeRepository)
        {
            _activeRepository = activeRepository;
        }

        public async Task<IEnumerable<Active>> GetListAsync()
        {
            var actives = await _activeRepository.GetListAsync();

            return actives;
        }
        public async Task<Active> Create(Active active)
        {
            var addActive = await _activeRepository.CreateAsync(active);
            return addActive;
        }
        public async Task<IEnumerable<Active>> GetListWithIncludesAsync(string[] include = null)
        {
            var addActive = await _activeRepository.GetListWithincludesAsync(include);
            return addActive;
        }
        public async Task<Active> UpdateAsync(Active active)
        {
           

            var updateActive =await _activeRepository.UpdateAsync(active);
            return updateActive;
        }
        public async Task<string> DeleteAsync(Active active)
        {
            var deleteActive = await _activeRepository.DeleteAsync(active);
            if (deleteActive != null)
                return "Successed Deleted";

            return "Un Success Delete";
        }
        public async Task<Active> Find(Expression<Func<Active , bool>> predicate , string[] inclueds = null)
        {
            var actvive = await _activeRepository.Find(predicate , inclueds);
            return actvive;
        }
    }
}
