using ArchiveTrackingSystem.Core.Dto.EmployeDtos;
using ArchiveTrackingSystem.Core.Entities;
using ArchiveTrackingSystem.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ArchiveTrackingSystem.Core.Routes.Route;

namespace ArchiveTrackingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly EmployeSrevices _employeSrevices;

        public EmployeController(IMapper mapper, EmployeSrevices employeSrevices)
        {
            _mapper=mapper;
            _employeSrevices=employeSrevices;
        }

        [HttpGet(EmployeRouting.List)]
        public async Task<IActionResult> GetList()
        {
            var emps = await _employeSrevices.GetListAsync();
            if (emps != null)
                return Ok(emps);

            return NotFound();
        }

        [HttpPost(EmployeRouting.Create)]
        public async Task<IActionResult> Create(EmployeAddDto employeAddDto)
        {
            if (employeAddDto == null)
                return BadRequest("No Data For Add");

            var empMapper = _mapper.Map<Employe>(employeAddDto);
            empMapper.Slug = await _employeSrevices.GetUniqueNameAsync(empMapper.Name);

            var addEmp = await _employeSrevices.CreateAsync(empMapper);
            if (addEmp != null)

                return Ok(addEmp);

            return BadRequest();
        }

        [HttpPut(EmployeRouting.Edit)]
        public async Task<IActionResult> Update(EmployeUpdateDto employeUpdateDto)
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
        public async Task<IActionResult> Delete(string slug)
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
