using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveTrackingSystem.Core.Dto.UserRoleDtos
{
    public class AddUserRoleDto
    {
        public int Id { get; set; }
        public List<RolesUser> rolesUsers { get; set; }
    }


    public class RolesUser
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public bool HasRole { get; set; }
    }

}
