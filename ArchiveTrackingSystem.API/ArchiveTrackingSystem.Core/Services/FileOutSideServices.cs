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
    public class FileOutSideServices
    {
        private readonly IBaseRepository<FileOutsideArchive> _fileOutSiedRepository;

        public FileOutSideServices(IBaseRepository<FileOutsideArchive> fileOutSiedRepository)
        {
            _fileOutSiedRepository=fileOutSiedRepository;
        }

        public async Task<IEnumerable<FileOutsideArchive>> GetListWithIncludesAsync(string[] includes = null)
        {
            includes = ["file", "employe"];

            var fileOutsideArchive = await _fileOutSiedRepository.GetListWithincludesAsync(includes);
            return fileOutsideArchive;
        }

        public async Task<FileOutsideArchive> CreateAsync(FileOutsideArchive fileOutsideArchive)
        {
            fileOutsideArchive.CreateAt = DateTime.Now;
           // fileOutsideArchive.Slug = await GetUniqueNameAsync(fileOutsideArchive.);
            var addFileOutside = await _fileOutSiedRepository.CreateAsync(fileOutsideArchive);
            return addFileOutside;
        }
        public async Task<FileOutsideArchive> Find(Expression<Func<FileOutsideArchive, bool>> predicate, string[] inclueds = null)
        {
            inclueds = ["file", "employe"];
            var fileOutsideArchive = await _fileOutSiedRepository.Find(predicate, inclueds);
            return fileOutsideArchive;
        }
        public async Task<string> DeleteAsync(FileOutsideArchive fileOutsideArchive)
        {
            var deletefileOutsideArchive = await _fileOutSiedRepository.DeleteAsync(fileOutsideArchive);
            if (deletefileOutsideArchive != null)
                return "Successed Deleted";

            return "Un Delete";


        }
        public async Task<FileOutsideArchive> UpateAsync(FileOutsideArchive fileOutsideArchive)
        {

            // البحث عن الموظف باستخدام Slug
            var existingFileOutside = await _fileOutSiedRepository.Find(e => e.Id == fileOutsideArchive.Id);

            if (existingFileOutside == null)
            {
                return null; // إذا لم يتم العثور على الموظف، أعد null
            }

            // تحديث الحقول المطلوبة
            existingFileOutside.ReturnDate = fileOutsideArchive.ReturnDate;
            existingFileOutside.Status = fileOutsideArchive.Status;
            existingFileOutside.UpdateAt = DateTime.Now;
            existingFileOutside.EmployeID = fileOutsideArchive.EmployeID;
            existingFileOutside.FileID = fileOutsideArchive.FileID;
            existingFileOutside.ReceiptDate = fileOutsideArchive.ReceiptDate;




            // تنفيذ عملية التحديث
            var updateFileOutside = await _fileOutSiedRepository.UpdateAsync(existingFileOutside);
            return updateFileOutside;


        }



    }
}
