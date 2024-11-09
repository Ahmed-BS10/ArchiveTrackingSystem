using ArchiveTrackingSystem.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveTrackingSystem.Core.Services
{

    public class RoleServices
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public RoleServices(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager=roleManager;
            _userManager=userManager;
        }

        #region Function
        public async Task<string> AddRoleAsync(string RoleName)
        {
            var Role = new Role()
            {
                Name = RoleName
            };

            var result = await _roleManager.CreateAsync(Role);
            if (result.Succeeded)
                return "Success";
            return "Failed";

        }
        public async Task<string> DeleteRoleAsync(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null) return "Not Found";

            var user = await _userManager.GetUsersInRoleAsync(role.Name);
            if (user != null) return "role Used";

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
                return "Success";

            var _erros = string.Join("-", result.Errors);
            return _erros;
        }
        public async Task<string> EditRoleAsync(int id, string name)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null) return "Not Found";

            role.Name = name;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
                return "Success";


            var _erros = string.Join("-", result.Errors);
            return _erros;

        }
        public async Task<IEnumerable<Role>> GetList()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            if (roles == null) return null;

            return roles;
        }

        #endregion


    }
}
