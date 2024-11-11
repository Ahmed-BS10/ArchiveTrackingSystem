using ArchiveTrackingSystem.Core.Dto.ActiveDtos;
using ArchiveTrackingSystem.Core.Entities;
using ArchiveTrackingSystem.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ArchiveTrackingSystem.Core.Routes.Route;

namespace ArchiveTrackingSystem.API.Controllers
{
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

        [HttpGet(ActiveRouting.Get)]
        public async Task<IActionResult> GetAsync(string slug)
        {
            var active = await _activeServices.Find(x => x.Slug == slug);

            var activeMapper = _mapper.Map<ActiveGetWithIccluedDto>(active);
            if (activeMapper != null)
                return Ok(activeMapper);



            return NotFound();
        }

        [HttpGet(ActiveRouting.List)]
        public async Task<IActionResult> GetListAsync()
        {
            var actives = await _activeServices.GetListAsync();

            var activeMapper = _mapper.Map<IEnumerable<ActiveGetWithIncludeDto>>(actives);
            if (activeMapper != null)
                return Ok(activeMapper);



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
        public async Task<IActionResult> CreateAsync([FromQuery]ActiveAddDto activeAddDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var active = await _activeServices.Find(x => x.Name == activeAddDto.Name);
            if (active != null)
                return BadRequest($"The name is {activeAddDto.Name}already used");

            var activeMapper = _mapper.Map<Active>(activeAddDto);
           

            var createActive = await _activeServices.Create(activeMapper);
            if (createActive != null)
                return Ok(createActive);

            return BadRequest(ModelState);


        }
        [HttpPut(ActiveRouting.Edit)]
        public async Task<IActionResult> UpdateAsync(ActiveUpdateDto activeUpdateDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var ActiveMapper = _mapper.Map<Active>(activeUpdateDto);
            ActiveMapper.UpdateAt = DateTime.Now;
            var UpdateActive = await _activeServices.UpdateAsync(ActiveMapper);

            if(UpdateActive != null)
                return Ok(UpdateActive);
            return BadRequest(ModelState);
        }

        [HttpDelete(ActiveRouting.Delete)]
        public async Task<IActionResult> DeleteAsync(string slug)
        {
            var active = await _activeServices.Find(x => x.Slug == slug);
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
