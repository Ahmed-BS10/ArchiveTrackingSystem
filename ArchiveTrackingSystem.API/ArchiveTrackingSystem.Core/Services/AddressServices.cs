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
        public async Task<Addrees> UpdateAsync(Addrees addrees)
        {


            // البحث عن الموظف باستخدام Slug
            var existingAddrees = await _addressRepository.Find(e => e.Id == addrees.Id);

            if (existingAddrees == null)
            {
                return null; // إذا لم يتم العثور على الموظف، أعد null
            }


            existingAddrees.City = addrees.City;
            existingAddrees.Dstrict =addrees.Dstrict;           
            existingAddrees.UpdateAt = DateTime.Now;


            // تنفيذ عملية التحديث
            var updatedAddrees = await _addressRepository.UpdateAsync(existingAddrees);
            return updatedAddrees;


        }


    }
}
