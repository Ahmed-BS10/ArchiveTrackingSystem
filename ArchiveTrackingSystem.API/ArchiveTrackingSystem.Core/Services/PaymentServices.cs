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
    public class PaymentServices
    {
        private readonly IBaseRepository<Payment> _paymnetRepoistory;

        public PaymentServices(IBaseRepository<Payment> paymnetRepoistory)
        {
            _paymnetRepoistory=paymnetRepoistory;
        }

        public async Task<IEnumerable<Payment>> GetListAsync()
        {
            var paymnets = await _paymnetRepoistory.GetListAsync();
            return paymnets;
        }
        public async Task<Payment> CreateAsync(Payment typePayment)
        {
            typePayment.CreateAt = DateTime.Now;
            typePayment.Slug = await GetUniqueNameAsync(typePayment.Name);
            var addPayment = await _paymnetRepoistory.CreateAsync(typePayment);
            return addPayment;
        }
        public async Task<Payment> UpdateAsync(Payment typePayment)
        {


            // البحث عن الموظف باستخدام Slug
            var existingPayment = await _paymnetRepoistory.Find(e => e.Slug == typePayment.Slug);

            if (existingPayment == null)
            {
                return null; // إذا لم يتم العثور على الموظف، أعد null
            }


            existingPayment.Name = typePayment.Name;
            existingPayment.Slug = await GetUniqueNameAsync(typePayment.Name);
            existingPayment.Number = typePayment.Number;
            existingPayment.Note = typePayment.Note;
            existingPayment.UpdateAt = DateTime.Now;
           


            // تنفيذ عملية التحديث
            var updatedActive = await _paymnetRepoistory.UpdateAsync(existingPayment);
            return updatedActive;



            var addPayment = await _paymnetRepoistory.UpdateAsync(typePayment);
            return addPayment;
        }
        public async Task<string> DeleteAsync(Payment typePayment)
        {
            var deletePaymnet = await _paymnetRepoistory.DeleteAsync(typePayment);
            if (deletePaymnet != null)
                return "Successed Deleted";

            return "Un Success Delete";
        }
        public async Task<Payment> Find(Expression<Func<Payment, bool>> predicate, string[] inclueds = null)
        {
            var paymnet = await _paymnetRepoistory.Find(predicate, inclueds);
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
