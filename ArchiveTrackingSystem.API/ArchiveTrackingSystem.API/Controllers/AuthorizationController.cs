using ArchiveTrackingSystem.Core.Constant;
using ArchiveTrackingSystem.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ArchiveTrackingSystem.Core.Routes.Route;

namespace ArchiveTrackingSystem.API.Controllers
{
    [Authorize(Roles = AuthorizationRoles.Admin + "," + AuthorizationRoles.Responsible)]

    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly AuthorizationServices _authorizationServices;

        public AuthorizationController(AuthorizationServices authorizationServices)
        {
            _authorizationServices=authorizationServices;
        }


        [HttpPost(AuthorizationRouting.Add)]
        public async Task<IActionResult> AddUserRole(int userId, List<string> RoleNames)
        {
            var addResult = await _authorizationServices.AddRolesToUserAsync(userId, RoleNames);

            switch (addResult)
            {
                case "UserNotFound": return NotFound("User Not Found");
                case "RolesAddedSuccessfully": return Ok("Roles Added Successfully");
                case "FailedToAddRoles": return BadRequest("Failed To Add Roles");
                default: return BadRequest(addResult);
            }
        }

        [HttpDelete(AuthorizationRouting.Delete)]
        public async Task<IActionResult> DeleteUserRole(int user, List<string> RoleNames)
        {
            var deleteResult = await _authorizationServices.DeleteRolesUserAsync(user, RoleNames);

            switch (deleteResult)
            {
                case "RolesRemovedSuccessfully": return Ok("Roles Removed Successfully");
                case "FailedToRemoveRoles": return BadRequest("Failed To Remove Roles");
                default: return BadRequest();
            }
        }

        [HttpPut(AuthorizationRouting.Edit)]
        public async Task<IActionResult> EditUserRole(int userId, List<string> RoleNames)
        {
            var editResult = await _authorizationServices.EditUserRolesAsync(userId, RoleNames);
            switch (editResult)
            {
                case "UserNotFound": return NotFound("User Not Found");
                case "FailedToRemoveOldRoles": return BadRequest("Failed To Remove Old Roles");
                case "RolesUpdatedSuccessfully": return Ok("Roles Updated Successfully");
                case "FailedToAddNewRoles": return BadRequest("Failed To Add New Roles");
                default: return BadRequest(editResult);
            }
        }
    }
}