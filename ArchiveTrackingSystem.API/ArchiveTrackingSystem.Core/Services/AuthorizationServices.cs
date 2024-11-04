using ArchiveTrackingSystem.Core.Dto.UserRoleDtos;
using ArchiveTrackingSystem.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveTrackingSystem.Core.Services
{
    public class AuthorizationServices
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;


        public AuthorizationServices(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager=userManager;
            _roleManager=roleManager;
        }

        public async Task<string> AddRolesToUserAsync(int userId, List<string> roleNames)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return "UserNotFound";



            var currentRoles = await _userManager.GetRolesAsync(user);

            // التحقق من وجود أي من الأدوار المطلوب إضافتها ضمن الأدوار الحالية
            var duplicateRoles = roleNames.Intersect(currentRoles).ToList();
            if (duplicateRoles.Any())
            {
                return $"Role(s) '{string.Join(", ", duplicateRoles)}' already assigned to the user.";
            }

           

            // إضافة الأدوار للمستخدم دفعة واحدة
            var result = await _userManager.AddToRolesAsync(user, roleNames);
            return result.Succeeded ? "RolesAddedSuccessfully" : "FailedToAddRoles";
        }
        public async Task<string> DeleteRolesUserAsync(int userId, List<string> roleNames)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null) return "UserNotFound";

            // حذف جميع الأدوار المحددة للمستخدم دفعة واحدة
            var result = await _userManager.RemoveFromRolesAsync(user, roleNames);
            return result.Succeeded ? "RolesRemovedSuccessfully" : "FailedToRemoveRoles";
        }


        public async Task<string> EditUserRolesAsync(int userId, List<string> roleNames)
        {
            //var transaction = await _context.Database.BeginTransactionAsync();
            //try
            //{
            //    var user = await _userManager.FindByIdAsync(request.Id.ToString());

            //    if (user == null)
            //        return "UserIsNull";

            //    var userRole = await _userManager.GetRolesAsync(user);
            //    var resultDelete = await _userManager.RemoveFromRolesAsync(user, userRole);
            //    if (!resultDelete.Succeeded) return "FailedToRemoveOldRoles";

            //    var selectRoles = request.RoleS.Where(x => x.HasRole == true).Select(x => x.RoleName);
            //    var resultAdd = await _userManager.AddToRolesAsync(user, selectRoles);
            //    if (!resultAdd.Succeeded) return "FailedToAddNewRoles";
            //    await transaction.CommitAsync();
            //    return "Success";
            //}
            //catch (Exception ex)
            //{
            //    await transaction.RollbackAsync();
            //    return "FailedToUpdateUserRoles";
            //}


            try
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null) return "UserNotFound";

                var userRoles = await _userManager.GetRolesAsync(user);
                var removeResult = await _userManager.RemoveFromRolesAsync(user, userRoles);

                if (!removeResult.Succeeded) return "FailedToRemoveOldRoles";

                var addResult = await _userManager.AddToRolesAsync(user, roleNames);
                return addResult.Succeeded ? "RolesUpdatedSuccessfully" : "FailedToAddNewRoles";
            }
            catch (Exception)
            {
                return "FailedToUpdateUserRoles";
            }


        }
    }
}
