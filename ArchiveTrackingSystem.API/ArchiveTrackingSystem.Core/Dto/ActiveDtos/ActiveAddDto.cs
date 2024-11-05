using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveTrackingSystem.Core.Dto.ActiveDtos
{
    public class ActiveAddDto
    {
        public string NumberActive { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string? Note { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public int? PaymentID { get; set; }

    }
}
