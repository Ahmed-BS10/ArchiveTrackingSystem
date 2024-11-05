using ArchiveTrackingSystem.Core.Entities;
using ArchiveTrackingSystem.Core.IRepoistories;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
