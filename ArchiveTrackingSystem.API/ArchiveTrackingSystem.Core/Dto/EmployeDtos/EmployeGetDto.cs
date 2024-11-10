using ArchiveTrackingSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveTrackingSystem.Core.Dto.EmployeDtos
{
    public class EmployeGetDto
    {
        public string Name { get; set; }
        public string job { get; set; }
        public string Deparatment { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }

    }
}
