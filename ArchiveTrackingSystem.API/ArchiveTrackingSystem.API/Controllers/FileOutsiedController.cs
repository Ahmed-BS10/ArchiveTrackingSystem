using ArchiveTrackingSystem.Core.Constant;
using ArchiveTrackingSystem.Core.Dto.FileDtos;
using ArchiveTrackingSystem.Core.Dto.FileOutsideArchive;
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

    public class FileOutsiedController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly FileOutSideServices _fileOutsideServices;

        public FileOutsiedController(FileOutSideServices fileOutsideServices, IMapper mapper)
        {
            _fileOutsideServices=fileOutsideServices;
            _mapper=mapper;
        }




        [HttpGet(FileOutsiedRouting.GetById)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var fileOutside = await _fileOutsideServices.Find(x => x.Id == id);
            if (fileOutside != null)
            {
                var fileMapper = _mapper.Map<FileOutsideGetDto>(fileOutside);
                return Ok(fileMapper);
            }


            return NotFound();
        }

        [HttpGet(FileOutsiedRouting.GetListWithincludes)]
        public async Task<IActionResult> GetListAsync()
        {
            var files = await _fileOutsideServices.GetListWithIncludesAsync();
            if (files != null)
            {
                var fileOutsiedMapper = _mapper.Map<IEnumerable<FileOutsideGetDto>>(files);
                return Ok(fileOutsiedMapper);
            }


            return NotFound();
        }

        [HttpPost(FileOutsiedRouting.Create)]
        public async Task<IActionResult> CreateAync(FileOutsideAddDto fileOutsideAddDto)
        {
            if (fileOutsideAddDto == null || !ModelState.IsValid)
                return BadRequest("Invalid data. Please check the input.");


            var fileOutsiedMapper = _mapper.Map<FileOutsideArchive>(fileOutsideAddDto);

            var addFileOutsied = await _fileOutsideServices.CreateAsync(fileOutsiedMapper);

            if (addFileOutsied != null)
                return Ok(fileOutsideAddDto);

            return BadRequest("An error occurred while adding the file.");
        }

        [HttpPut(FileOutsiedRouting.Edit)]
        public async Task<IActionResult> UpdateAync(FileOutsideUpdateDto fileOutsideUpdateDto)
        {
            if (fileOutsideUpdateDto == null)
                return BadRequest("No Data For Update");

            var fileOutsiedMapper = _mapper.Map<FileOutsideArchive>(fileOutsideUpdateDto);
            var updateFileOutsied = await _fileOutsideServices.UpateAsync(fileOutsiedMapper);

            if (updateFileOutsied != null)
                return Ok(updateFileOutsied);

            return BadRequest();
        }
        [HttpDelete(FileOutsiedRouting.Delete)]
        public async Task<IActionResult> DeleteAync(int id)
        {
            var fileOutsied = await _fileOutsideServices.Find(x => x.Id == id);
            if (fileOutsied == null)
                return NotFound();

            var deletefileOutsied = await _fileOutsideServices.DeleteAsync(fileOutsied);
            if (deletefileOutsied == "Successed Deleted")
                return Ok(deletefileOutsied);

            else if (deletefileOutsied ==  "Un Delete")
                return BadRequest(deletefileOutsied);

            return BadRequest();
        }
    }
}
