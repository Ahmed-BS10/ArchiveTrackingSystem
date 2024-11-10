using ArchiveTrackingSystem.Core.Entities;
using ArchiveTrackingSystem.Core.Helper;
using ArchiveTrackingSystem.Core.IRepoistories;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveTrackingSystem.Core.Services
{
    public class FileServices
    {
        private readonly IBaseRepository<ArchiveTrackingSystem.Core.Entities.File> _fileRepository;
        private readonly IBaseRepository<Addrees> _addreesRepository;

        public FileServices(IBaseRepository<Entities.File> fileRepository, IBaseRepository<Addrees> addreesRepository)
        {
            _fileRepository=fileRepository;
            _addreesRepository=addreesRepository;
        }


        public async Task<IEnumerable<Entities.File>> GetListAsync()
        {
            return await _fileRepository.GetListAsync();
        }
        public async Task<IEnumerable<Entities.File>> GetListWithIncludesAsync(string[] includes = null)
        {
            includes = ["typePayment", "activte", "addrees", "archive"];

            var files = await _fileRepository.GetListWithincludesAsync(includes);
            return files;
        }

        public async Task<Entities.File> CreateAsync(Entities.File file)
        {
            file.CreateAt = DateTime.Now;
            file.Slug = await GetUniqueNameAsync(file.Name);
            var addFile = await _fileRepository.CreateAsync(file);
            return addFile;

        }
        public async Task<string> DeleteAsync(Entities.File file)
        {
            var deleteFile = await _fileRepository.DeleteAsync(file);
            if (deleteFile != null)
                return "Successed Deleted";

            return "Un Delete";


        }
        public async Task<Entities.File> UpateAsync(Entities.File file )
        {

            // البحث عن الموظف باستخدام Slug
            var existingFile = await _fileRepository.Find(e => e.Slug == file.Slug);

            if (existingFile == null)
            {
                return null; // إذا لم يتم العثور على الموظف، أعد null
            }

            // تحديث الحقول المطلوبة
            existingFile.Name = file.Name;
            existingFile.Slug = await GetUniqueNameAsync(file.Name);
            existingFile.TaxNumber = file.TaxNumber;
            existingFile.EmployeeName = file.EmployeeName;
            existingFile.CommercialNumber = file.CommercialNumber;
            existingFile.DocumentCount = file.DocumentCount;
            existingFile.Notes = file.Notes;
            existingFile.ActiveID = file.ActiveID;
            existingFile.AddressID = file.AddressID;
            existingFile.PaymentID = file.PaymentID;
            existingFile.FileStatus = file.FileStatus;
            existingFile.UpdateAt = DateTime.Now;


            // تنفيذ عملية التحديث
            var updatedEmp = await _fileRepository.UpdateAsync(existingFile);
            return updatedEmp;


        }
        public async Task<Entities.File> Find(Expression<Func<Entities.File, bool>> predicate, string[] inclueds = null)
        {
            inclueds = ["typePayment" , "activte" , "addrees" , "archive"];
            var file = await _fileRepository.Find(predicate, inclueds);
            return file;
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
