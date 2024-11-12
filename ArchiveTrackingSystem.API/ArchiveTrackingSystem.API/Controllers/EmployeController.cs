using ArchiveTrackingSystem.Core.Constant;
using ArchiveTrackingSystem.Core.Dto.ActiveDtos;
using ArchiveTrackingSystem.Core.Dto.EmployeDtos;
using ArchiveTrackingSystem.Core.Entities;
using ArchiveTrackingSystem.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ArchiveTrackingSystem.Core.Routes.Route;

namespace ArchiveTrackingSystem.API.Controllers
{
    [ApiController]
    [Authorize(Roles = AuthorizationRoles.Admin + "," + AuthorizationRoles.Responsible)]

    public class EmployeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly EmployeSrevices _employeSrevices;

        public EmployeController(IMapper mapper, EmployeSrevices employeSrevices)
        {
            _mapper=mapper;
            _employeSrevices=employeSrevices;
        }

        [HttpGet(EmployeRouting.GetBySlug)]
        public async Task<IActionResult> GetAsync(string slug)
        {
            var emp = await _employeSrevices.Find(x => x.Slug == slug);

            var empMapper = _mapper.Map<EmployeGetDto>(emp);
            if (empMapper != null)
                return Ok(empMapper);

            return NotFound();
        }

        [HttpGet(EmployeRouting.List)]
        public async Task<IActionResult> GetListAsync()
        {
            var emps = await _employeSrevices.GetListAsync();
            var empMapper = _mapper.Map<IEnumerable<EmployeGetDto>>(emps);
            
            if (empMapper != null)
                return Ok(empMapper);

            return NotFound();
        }

        [HttpPost(EmployeRouting.Create)]
        public async Task<IActionResult> CreateAsync(EmployeAddDto employeAddDto)
        {
            if (employeAddDto == null)
                return BadRequest("No Data For Add");

            var emp = await _employeSrevices.Find(x => x.Name == employeAddDto.Name);
            if (emp != null)
                return BadRequest($"The name is {employeAddDto.Name}already used");

            var empMapper = _mapper.Map<Employe>(employeAddDto);

            var addEmp = await _employeSrevices.CreateAsync(empMapper);
            if (addEmp != null)

                return Ok(addEmp);

            return BadRequest();
        }

        [HttpPut(EmployeRouting.Edit)]
        public async Task<IActionResult> UpdateAsync(EmployeUpdateDto employeUpdateDto)
        {
            if (employeUpdateDto == null)
                return BadRequest("No Data For Update");

            var empMapper = _mapper.Map<Employe>(employeUpdateDto);
            var updateEmp = await _employeSrevices.UpateAsync(empMapper);

            if (updateEmp != null)
                return Ok(updateEmp);

            return BadRequest();
        }

        [HttpDelete(EmployeRouting.Delete)]
        public async Task<IActionResult> DeleteAsync(string slug)
        {
            var emp = await _employeSrevices.Find(x => x.Slug == slug);
            if (emp == null)
                return NotFound();

            var deleteEmp = await _employeSrevices.DeleteAsync(emp);
            if (deleteEmp == "Successed Deleted")
                return Ok(deleteEmp);

            else if (deleteEmp ==  "Un Delete")
                return BadRequest(deleteEmp);

            return BadRequest();

        }


    }
}
