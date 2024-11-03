using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveTrackingSystem.Core.Dto.RoleDtos
{
    public class RoleAddDto
    {
        [Required]
        public string Name { get; set; }
    }


}
