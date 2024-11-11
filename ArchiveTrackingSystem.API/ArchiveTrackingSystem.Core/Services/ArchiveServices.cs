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
    public class ArchiveServices
    {
        private readonly IBaseRepository<Archive> _archiveRepository;

        public ArchiveServices(IBaseRepository<Archive> archiveRepository)
        {
            this._archiveRepository=archiveRepository;
        }

        public async Task<Archive> CreateAsync(Archive archive)
        {
            archive.CreateAt = DateTime.Now;
            archive.Slug = await GetUniqueNameAsync(archive.Name);
            var addEmp = await _archiveRepository.CreateAsync(archive);
            return addEmp;
        }
        public async Task<IEnumerable<Archive>> GetListWithIncludesAsync(string[] includes = null)
        {
            includes = ["Files"];

            var archives = await _archiveRepository.GetListWithincludesAsync(includes);
            return archives;
        }
        public async Task<string> DeleteAsync(Archive archive)
        {
            var deleteArchive = await _archiveRepository.DeleteAsync(archive);
            if (deleteArchive != null)
                return "Successed Deleted";

            return "Un Delete";


        }
        public async Task<Archive> UpateAsync(Archive archive)
        {

            // البحث عن الموظف باستخدام Slug
            var existingArchive = await _archiveRepository.Find(e => e.Slug == archive.Slug);

            if (existingArchive == null)
            {
                return null; // إذا لم يتم العثور على الموظف، أعد null
            }

            // تحديث الحقول المطلوبة
            existingArchive.Name = archive.Name;
            existingArchive.Slug = await GetUniqueNameAsync(archive.Name);
            existingArchive.UpdateAt = DateTime.Now;

            // تنفيذ عملية التحديث
            var updatedArchive = await _archiveRepository.UpdateAsync(existingArchive);
            return updatedArchive;


        }
        public async Task<Archive> Find(Expression<Func<Archive, bool>> predicate, string[] inclueds = null)
        {
            inclueds = ["Files"];
            var archive = await _archiveRepository.Find(predicate, inclueds);
            return archive;
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
