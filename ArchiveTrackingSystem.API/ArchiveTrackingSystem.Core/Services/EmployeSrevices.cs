using ArchiveTrackingSystem.Core.Entities;
using ArchiveTrackingSystem.Core.Helper;
using ArchiveTrackingSystem.Core.IRepoistories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveTrackingSystem.Core.Services
{
    public class EmployeSrevices
    {
        private readonly IBaseRepository<Employe> _employeRepoistory;

        public EmployeSrevices(IBaseRepository<Employe> employeRepoistory)
        {
            _employeRepoistory=employeRepoistory;
        }
        public async Task<Employe> CreateAsync(Employe employe)
        {
            var addEmp = await _employeRepoistory.CreateAsync(employe);
            return addEmp;
        }
        public async Task<IEnumerable<Employe>> GetListAsync()
        {
            var emps = await _employeRepoistory.GetListAsync();
            return emps;

        }
        public async Task<string> DeleteAsync(Employe employe)
        {
            var deleteEmp = await _employeRepoistory.DeleteAsync(employe);
            if (deleteEmp != null)
                return "Successed Deleted";

            return "Un Delete";


        }
        public async Task<Employe> UpateAsync(Employe employe)
        {

            // البحث عن الموظف باستخدام Slug
            var existingEmploye = await _employeRepoistory.Find(e => e.Slug == employe.Slug);

            if (existingEmploye == null)
            {
                return null; // إذا لم يتم العثور على الموظف، أعد null
            }

            // تحديث الحقول المطلوبة
            existingEmploye.Name = employe.Name;
            existingEmploye.Slug = await GetUniqueNameAsync(employe.Name);
            existingEmploye.job = employe.job;
            existingEmploye.Deparatment = employe.Deparatment;

            // تنفيذ عملية التحديث
            var updatedEmp = await _employeRepoistory.UpdateAsync(existingEmploye);
            return updatedEmp;


        }
        public async Task<Employe> Find(Expression<Func<Employe, bool>> predicate, string[] inclueds = null)
        {
            var paymnet = await _employeRepoistory.Find(predicate, inclueds);
            return paymnet;
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
