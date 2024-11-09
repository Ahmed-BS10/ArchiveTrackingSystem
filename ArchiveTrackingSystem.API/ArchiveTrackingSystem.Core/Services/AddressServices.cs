using ArchiveTrackingSystem.Core.Entities;
using ArchiveTrackingSystem.Core.IRepoistories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveTrackingSystem.Core.Services
{
    public class AddressServices
    {
        private readonly IBaseRepository<Addrees> _addressRepository;

        public AddressServices(IBaseRepository<Addrees> addressRepository)
        {
            _addressRepository=addressRepository;
        }


        public async Task<Addrees> CreateAsync(Addrees addrees)
        {
            addrees.CreateAt = DateTime.Now;
            
            var addAddress = await _addressRepository.CreateAsync(addrees);
            return addAddress;
        }
    }
}
