using ArchiveTrackingSystem.Core.Dto.ActiveDtos;
using ArchiveTrackingSystem.Core.Dto.EmployeDtos;
using ArchiveTrackingSystem.Core.Dto.FileDtos;
using ArchiveTrackingSystem.Core.Entities;
using ArchiveTrackingSystem.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ArchiveTrackingSystem.Core.Routes.Route;

namespace ArchiveTrackingSystem.API.Controllers
{
   
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly FileServices _fileServices;
        private readonly AddressServices _addressServices;

        public FileController(FileServices fileServices, IMapper mapper, AddressServices addressServices)
        {
            _fileServices=fileServices;
            _mapper=mapper;
            _addressServices=addressServices;
        }


        [HttpGet(FileRouting.List)]
        public async Task<IActionResult> GetList()
        {
            var files = await _fileServices.GetListAsync();
            if (files != null)
            {
                var fileMapper = _mapper.Map<IEnumerable<FileGetDto>>(files);
                return Ok(fileMapper);
            }
               

            return NotFound();
        }

        [HttpGet(FileRouting.GetListWithincludes)]
        public async Task<IActionResult> GetListWithincludesAsync([FromQuery] string[] inclueds = null)
        {
            var flies = await _fileServices.GetListWithIncludesAsync(inclueds);

            var fileMapper = _mapper.Map<IEnumerable<FileGetWithIncludeDto>>(flies);
            if (fileMapper != null)
                return Ok(fileMapper);



            return NotFound();
        }


        [HttpPost(FileRouting.Create)]
        public async Task<IActionResult> Create(FileAddDto fileAddDto)
        {
            if (fileAddDto == null || !ModelState.IsValid)
                return BadRequest("Invalid data. Please check the input.");

           
            var fileMapper = _mapper.Map<ArchiveTrackingSystem.Core.Entities.File>(fileAddDto);
 
            if (fileAddDto.address != null)
            {
                var addressEntity = _mapper.Map<Addrees>(fileAddDto.address);

                var createdAddress = await _addressServices.CreateAsync(addressEntity);

                fileMapper.AddressID = createdAddress.Id;
            }

            var addFile = await _fileServices.CreateAsync(fileMapper);

            if (addFile != null)
                return CreatedAtAction(nameof(Create), new { id = addFile.Id }, addFile);

            return BadRequest("An error occurred while adding the file.");
        }

        [HttpPut(FileRouting.Edit)]
        public async Task<IActionResult> Update(FileUpdateDto fileUpdateDto)
        {
            if (fileUpdateDto == null)
                return BadRequest("No Data For Update");

            var fileMapper = _mapper.Map<Core.Entities.File>(fileUpdateDto);
            var updateFile = await _fileServices.UpateAsync(fileMapper);

            if (updateFile != null)
                return Ok(updateFile);

            return BadRequest();
        }
        [HttpDelete(FileRouting.Delete)]
        public async Task<IActionResult> Delete(string slug)
        {
            var file = await _fileServices.Find(x => x.Slug == slug);
            if (file == null)
                return NotFound();

            var deleteEmp = await _fileServices.DeleteAsync(file);
            if (deleteEmp == "Successed Deleted")
                return Ok(deleteEmp);

            else if (deleteEmp ==  "Un Delete")
                return BadRequest(deleteEmp);

            return BadRequest();
        }
       

    }
}
