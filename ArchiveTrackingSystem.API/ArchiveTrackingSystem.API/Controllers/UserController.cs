using ArchiveTrackingSystem.Core.Dto.UserDtos;
using ArchiveTrackingSystem.Core.Entities;
using ArchiveTrackingSystem.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using static ArchiveTrackingSystem.Core.Routes.Route;

namespace ArchiveTrackingSystem.API.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserServices _userServices;
        private IMapper _mapper;

        public UserController(UserServices userServices, IMapper mapper)
        {
            _userServices=userServices;
            _mapper=mapper;
        }

        [HttpPost(UserRouting.Create)]
        public async Task<IActionResult> CreateAsync([FromBody]UserAddDto userAddDto , string role)
        {
            var user = _mapper.Map<User>(userAddDto);
            var addResult = await _userServices.AddAsycn(user, userAddDto.Password, role);

            switch (addResult)
            {
                case "Email Already Exists": return BadRequest("Email Already Exists");
                case "User Name Already Exists": return BadRequest("Name Already Exists");
                case "User PhoneNumber Already Exists": return BadRequest("User PhoneNumber Already Exists");
                case "ThisRoleNotExists": return BadRequest($"This Role {role} Not Exists");
                default: return Ok(addResult);
            }
        }

        [HttpGet(UserRouting.List)]
        public async Task<IActionResult> GetList()
        {
            var users = await _userServices.GetListAsync();
            var userMapper = _mapper.Map<IEnumerable<UserGetDto>>(users);

            if (userMapper != null)
                return Ok(userMapper);

            return NotFound();

        }

        [HttpPut(UserRouting.Edit)]
        public async Task<IActionResult> Edit(UserEditDto userEditDto)
        {
            var userMapper = _mapper.Map<User>(userEditDto);

            var editResult = await _userServices.EditAsync(userMapper);

            switch (editResult)
            {
                case "User Not Found": return NotFound("User Not Found");
                case "User Name Already Exists": return BadRequest("User Name Already Exists");
                case "User PhoneNumber Already Exists": return BadRequest("User PhoneNumber Already Exists");
                case "Successed": return Ok($"Id Edited is {userMapper.Id}");
                default: return BadRequest(editResult);
            }
        }

        [HttpDelete(UserRouting.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteResult = await _userServices.DeleteAsync(id);

            switch(deleteResult)
            {
                case "User Not Found": return NotFound("User Not Found");
                case "Deleteed Success": return BadRequest("Deleteed Success");
                default: return BadRequest(deleteResult);
            }
        }

      
    }
}
