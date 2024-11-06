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
    public class PaymentServices
    {
        private readonly IBaseRepository<TypePayment> _paymnetRepoistory;

        public PaymentServices(IBaseRepository<TypePayment> paymnetRepoistory)
        {
            _paymnetRepoistory=paymnetRepoistory;
        }

        public async Task<IEnumerable<TypePayment>> GetListAsync()
        {
            var paymnets = await _paymnetRepoistory.GetListAsync();
            return paymnets;
        }
        public async Task<TypePayment> CreateAsync(TypePayment typePayment)
        {
            var addPayment = await _paymnetRepoistory.CreateAsync(typePayment);
            return addPayment;
        }
        public async Task<TypePayment> UpdateAsync(TypePayment typePayment)
        {
            var addPayment = await _paymnetRepoistory.UpdateAsync(typePayment);
            return addPayment;
        }
        public async Task<string> DeleteAsync(TypePayment typePayment)
        {
            var deletePaymnet = await _paymnetRepoistory.DeleteAsync(typePayment);
            if (deletePaymnet != null)
                return "Successed Deleted";

            return "Un Success Delete";
        }
        public async Task<TypePayment> Find(Expression<Func<TypePayment, bool>> predicate, string[] inclueds = null)
        {
            var paymnet = await _paymnetRepoistory.Find(predicate, inclueds);
            return paymnet;
        }


    }
}
