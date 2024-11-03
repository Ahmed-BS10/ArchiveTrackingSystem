using ArchiveTrackingSystem.Core.Dto.RoleDtos;
using ArchiveTrackingSystem.Core.Entities;
using ArchiveTrackingSystem.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Writers;
using static ArchiveTrackingSystem.Core.Routes.Route;

namespace ArchiveTrackingSystem.API.Controllers
{

    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleServices _roleServices;
        private readonly IMapper _mapper;

        public RoleController(RoleServices roleServices, IMapper mapper)
        {
            _roleServices=roleServices;
            _mapper=mapper;
        }

        [HttpGet(RoleRouting.List)]
        public async Task<IActionResult> GetList()
        {
            var roles = await _roleServices.GetList(); // افتراض أنك تحصل على IEnumerable<Role>
            var roleMapper = _mapper.Map<IEnumerable<RoleGetDto>>(roles); // تحويل إلى Dto

            if (roles != null)
                return Ok(roleMapper);

            return BadRequest();
        }

        [HttpPost(RoleRouting.Create)]
        public async Task<IActionResult> Create(string Name)
        {
            if (Name != null)
            {
                var addResult = await _roleServices.AddRoleAsync(Name);
                switch (addResult) {
                    case "Success": return Ok(addResult);
                    case "Failed": return BadRequest(addResult);
                    default: return BadRequest();
                }

            }

            return BadRequest();

          
        }


        [HttpPut(RoleRouting.Edit)]
        public async Task<IActionResult> Edit(int id , string name)
        {
            if (name != null || id == 0)
            {
                var editResult = await _roleServices.EditRoleAsync(id, name);
                switch (editResult)
                {
                    case "Success": return Ok(editResult);
                    default: return BadRequest();
                }

            }

            return BadRequest("ID or Name in null");


        }

        [HttpDelete(RoleRouting.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteResult = await _roleServices.DeleteRoleAsync(id);

            switch (deleteResult)
            {
                case "Not Found": return NotFound("Not Found");
                case "role Used": return BadRequest("role Used");
                case "Success": return Ok($"Id Delete {id}");
                default: return BadRequest(deleteResult);
            }
        }


    }

}
