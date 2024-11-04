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
   
    public class UserServices
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly AuthenticatiomServices _authenticatiomServices;

        public UserServices(UserManager<User> userManager, RoleManager<Role> roleManager, AuthenticatiomServices authenticatiomServices)
        {
            _userManager=userManager;
            _roleManager=roleManager;
            _authenticatiomServices=authenticatiomServices;
        }

        public async Task<IEnumerable<User>> GetListAsync()
        {

            var users = await _userManager.Users.AsQueryable().AsNoTracking().ToListAsync();

            return users;


        }
        public async Task<string> AddAsycn(User inputUser, string password , string role)
        {
            var userByEmail = await _userManager.FindByEmailAsync(inputUser.Email);
            if (userByEmail != null)
                return "Email Already Exists";

           

            var userByName = await _userManager.FindByNameAsync(inputUser.UserName);
            if (userByName != null)
                return "User Name Already Exists";



            var userByPhone = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == inputUser.PhoneNumber);
            if (userByPhone != null)
                return "User PhoneNumber Already Exists";



            var roleExist = await _roleManager.RoleExistsAsync(role);
            if (!roleExist)
                return "ThisRoleNotExists";



            var reslutCreate = await _userManager.CreateAsync(inputUser, password);



            if (!reslutCreate.Succeeded)
                return string.Join("; ", reslutCreate.Errors.Select(e => e.Description));




            await _userManager.AddToRoleAsync(inputUser, role);

            var token = await _authenticatiomServices.CreateToken(inputUser);

            return token;

        }
        public async Task<string> EditAsync(User user)
        {
            var userFind = await _userManager.FindByIdAsync(user.Id.ToString());
            if (userFind == null)
                return "User Not Found";

            if (user.UserName != userFind.UserName)
            {
                var userByName = await _userManager.FindByNameAsync(user.UserName);
                if (userByName != null)
                    return "User Name Already Exists";
            }


            if (user.PhoneNumber != userFind.PhoneNumber)
            {
                var userByPhone = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == user.PhoneNumber);
                if (userByPhone != null)
                    return "User PhoneNumber Already Exists";
            }


            userFind.UserName = user.UserName;
            userFind.PhoneNumber = user.PhoneNumber;
            userFind.FirstName = user.FirstName; 
            userFind.LastName = user.LastName;
            userFind.Email = user.Email;
            var resultEdit = await _userManager.UpdateAsync(userFind);

            if (!resultEdit.Succeeded)
            {
                return string.Join("; ", resultEdit.Errors.Select(e => e.Description));
            }

            return "Successed";
        }
        public async Task<string> DeleteAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return "User Not Found";


            await _userManager.DeleteAsync(user);
            return "Deleteed Success";

        }
        
    }
}
