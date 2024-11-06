using ArchiveTrackingSystem.Core.Dto.ActiveDtos;
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
    public class ActiveController : ControllerBase
    {
        private readonly ActiveServices _activeServices;
        private readonly IMapper _mapper;

        public ActiveController(ActiveServices activeServices, IMapper mapper = null)
        {
            _activeServices=activeServices;
            _mapper=mapper;
        }


        [HttpGet(ActiveRouting.List)]
        public async Task<IActionResult> GetListAsync()
        {
            var emps = await _activeServices.GetListAsync();

            var empsMapper = _mapper.Map<IEnumerable<ActiveGetDto>>(emps);
            if (empsMapper != null)
                return Ok(empsMapper);



            return NotFound();
        }

        [HttpGet(ActiveRouting.GetListWithincludes)]
        public async Task<IActionResult> GetListWithincludesAsync([FromQuery] string[] inclueds = null)
        {
            var emps = await _activeServices.GetListWithIncludesAsync(inclueds);

            var empsMapper = _mapper.Map<IEnumerable<ActiveGetWithIccluedDto>>(emps);
            if (empsMapper != null)
                return Ok(empsMapper);



            return NotFound();
        }

        [HttpPost(ActiveRouting.Create)]
        public async Task<IActionResult> Create([FromQuery]ActiveAddDto activeAddDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var activeMapper = _mapper.Map<Active>(activeAddDto);

            var createActive = await _activeServices.Create(activeMapper);
            if (createActive != null)
                return Ok(createActive);

            return BadRequest(ModelState);


        }
        [HttpPut(ActiveRouting.Edit)]
        public async Task<IActionResult> Update(ActiveUpdateDto activeUpdateDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var ActiveMapper = _mapper.Map<Active>(activeUpdateDto);
            var UpdateActive = await _activeServices.UpdateAsync(ActiveMapper);

            if(UpdateActive != null)
                return Ok(UpdateActive);
            return BadRequest(ModelState);
        }

        [HttpDelete(ActiveRouting.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var active = await _activeServices.Find(x => x.Id == id);
            if (active != null)
            {
                var deleteActive = await _activeServices.DeleteAsync(active);

                if (deleteActive == "Successed Deleted")
                    return Ok(active);

               
            }

            return BadRequest("It Can Not Delete Null Active");

        }
    }
}
