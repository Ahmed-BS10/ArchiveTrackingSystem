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
            if (activeAddDto == null)
                return BadRequest(ModelState);

            var activeMapper = _mapper.Map<Active>(activeAddDto);

            var createActive = await _activeServices.Create(activeMapper);
            if (createActive != null)
                return Ok(createActive);

            return BadRequest(ModelState);


        }
    }
}
