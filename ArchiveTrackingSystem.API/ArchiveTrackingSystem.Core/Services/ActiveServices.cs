using ArchiveTrackingSystem.Core.Entities;
using ArchiveTrackingSystem.Core.Helper;
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
            active.CreateAt = DateTime.Now;
            active.Slug = await GetUniqueNameAsync(active.Name);
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
          
           
            // البحث عن الموظف باستخدام Slug
            var existingActive = await _activeRepository.Find(e => e.Slug == active.Slug);

            if (existingActive == null)
            {
                return null; // إذا لم يتم العثور على الموظف، أعد null
            }


            existingActive.Name = active.Name;
            existingActive.Slug = await GetUniqueNameAsync(active.Name);
            existingActive.NumberActive = active.NumberActive;
            existingActive.Type = active.Type;
            existingActive.Note = active.Note;
            existingActive.UpdateAt = DateTime.Now;
            existingActive.PaymentID = active.PaymentID;
           

            // تنفيذ عملية التحديث
            var updatedActive = await _activeRepository.UpdateAsync(existingActive);
            return updatedActive;


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
        public async Task<string> GetUniqueNameAsync(string arabicName)
        {
            string baseName = EncryptionName.ConvertArabicToEnglish(arabicName); // تحويل الاسم إلى إنجليزي
            string uniqueName = baseName;
            int counter = 1;

            // التحقق من تكرار الاسم في قاعدة البيانات
            while (await Find(x => x.Slug == uniqueName) != null)
            {
                uniqueName = $"{baseName}{counter}";
                counter++;
            }

            return uniqueName;
        }
    }
}
