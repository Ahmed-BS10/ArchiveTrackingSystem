using ArchiveTrackingSystem.Core.Constant;
using ArchiveTrackingSystem.Core.Dto.ArchiveDtos;
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

    public class ArchiveController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ArchiveServices _archiveServices;

        public ArchiveController(IMapper mapper, ArchiveServices archiveServices)
        {
            _mapper=mapper;
            _archiveServices=archiveServices;
        }


        [HttpGet(ArchiveRouting.GetBySlug)]
        public async Task<IActionResult> GetAsync(string slug)
        {
            var archive = await _archiveServices.Find(x => x.Slug == slug);

            var archiveMapper = _mapper.Map<ArchiveGetDto>(archive);
            if (archiveMapper != null)
                return Ok(archiveMapper);

            return NotFound();
        }

        [HttpGet(ArchiveRouting.List)]
        public async Task<IActionResult> GetListAsync()
        {
            var archives = await _archiveServices.GetListWithIncludesAsync();
            var archiveMapper = _mapper.Map<IEnumerable<ArchiveGetDto>>(archives);

            if (archiveMapper != null)
                return Ok(archiveMapper);

            return NotFound();
        }

        [HttpPost(ArchiveRouting.Create)]
        public async Task<IActionResult> CreateAsync(ArchiveAddDto archiveAddDto)
        {
            if (archiveAddDto == null)
                return BadRequest("No Data For Add");

            var archive = await _archiveServices.Find(x => x.Name == archiveAddDto.Name);
            if (archive != null)
                return BadRequest($"The name is {archiveAddDto.Name}already used");

            var archiveMapper = _mapper.Map<Archive>(archiveAddDto);

            var addArchive = await _archiveServices.CreateAsync(archiveMapper);
            if (addArchive != null)

                return Ok(addArchive);

            return BadRequest();
        }

        [HttpPut(ArchiveRouting.Edit)]
        public async Task<IActionResult> UpdateAsync(ArchiveUpdateDto archiveUpdateDto)
        {
            if (archiveUpdateDto == null)
                return BadRequest("No Data For Update");

            var archiveMapper = _mapper.Map<Archive>(archiveUpdateDto);
            var updateArchive = await _archiveServices.UpateAsync(archiveMapper);

            if (updateArchive != null)
                return Ok(updateArchive);

            return BadRequest();
        }

        [HttpDelete(ArchiveRouting.Delete)]
        public async Task<IActionResult> DeleteAsync(string slug)
        {
            var archive = await _archiveServices.Find(x => x.Slug == slug);
            if (archive == null)
                return NotFound();

            var deleteArchive = await _archiveServices.DeleteAsync(archive);
            if (deleteArchive == "Successed Deleted")
                return Ok(deleteArchive);

            else if (deleteArchive ==  "Un Delete")
                return BadRequest(deleteArchive);

            return BadRequest();

        }

    }
}
