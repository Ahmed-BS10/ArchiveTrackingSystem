using ArchiveTrackingSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveTrackingSystem.Core.Dto.PaymentDtos
{
    public class PaymentGetDto
    {
       
        public int Number { get; set; }
        public string Name { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string Note { get; set; }

       
    }
}
