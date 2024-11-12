using ArchiveTrackingSystem.Core.Constant;
using ArchiveTrackingSystem.Core.Dto.ActiveDtos;
using ArchiveTrackingSystem.Core.Dto.EmployeDtos;
using ArchiveTrackingSystem.Core.Dto.FileDtos;
using ArchiveTrackingSystem.Core.Dto.RoleDtos;
using ArchiveTrackingSystem.Core.Entities;
using ArchiveTrackingSystem.Core.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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



        [Authorize(Roles = AuthorizationRoles.Admin + "," + AuthorizationRoles.Responsible)]
        [HttpGet(FileRouting.GetBySlug)]
        public async Task<IActionResult> GetAsync(string slug)
        {
            var file = await _fileServices.Find(x => x.Slug == slug);
            if (file != null)
            {
                var fileMapper = _mapper.Map<FileGetWithIncludeDto>(file);
                return Ok(fileMapper);
            }


            return NotFound();
        }




        [Authorize(Roles = AuthorizationRoles.Admin + "," + AuthorizationRoles.Responsible)]
        [HttpGet(FileRouting.List)]
        public async Task<IActionResult> GetListAsync()
        {
            var files = await _fileServices.GetListAsync();
            if (files != null)
            {
                var fileMapper = _mapper.Map<IEnumerable<FileGetDto>>(files);
                return Ok(fileMapper);
            }


            return NotFound();
        }




        [Authorize(Roles = AuthorizationRoles.Admin + "," + AuthorizationRoles.Responsible + "," + AuthorizationRoles.Show)]
        [HttpGet("GetListWithincludes")]
        public async Task<IActionResult> GetListWithincludesAsync([FromQuery] DateTime? startDate = null,[FromQuery] DateTime? endDate = null,[FromQuery] string archive = null,[FromQuery] string payment = null,[FromQuery] string city = null,[FromQuery] string activte = null,[FromQuery] string Dstrict = null)
        {
            // استدعاء الخدمة مع تمرير معاملات الفلترة
            var files = await _fileServices.GetListWithIncludesAsync(

                startDate: startDate,
                endDate: endDate,
                archive: archive,
                activte: activte,
                payment: payment,
                city: city,
                Dstrict: Dstrict
            );

            // تحويل النتائج إلى DTO باستخدام AutoMapper
            var fileMapper = _mapper.Map<IEnumerable<FileGetWithIncludeDto>>(files);

            // التحقق من النتيجة قبل الإرجاع
            if (fileMapper != null && fileMapper.Any())
                return Ok(fileMapper);

            return NotFound();
        }




        [Authorize(Roles = AuthorizationRoles.Admin + "," + AuthorizationRoles.Responsible + "," +AuthorizationRoles.Create)]
        [HttpPost(FileRouting.Create)]
        public async Task<IActionResult> CreateAync(FileAddDto fileAddDto)
        {
            if (fileAddDto == null || !ModelState.IsValid)
                return BadRequest("Invalid data. Please check the input.");


            var file = await _fileServices.Find(x => x.FileNumber == fileAddDto.FileNumber);
            if (file != null)
                return BadRequest($"The name is {fileAddDto.FileNumber}already used");



            var fileMapper = _mapper.Map<ArchiveTrackingSystem.Core.Entities.File>(fileAddDto);

            if (fileAddDto.address != null)
            {
                var addressEntity = _mapper.Map<Addrees>(fileAddDto.address);

                var createdAddress = await _addressServices.CreateAsync(addressEntity);

                fileMapper.AddressID = createdAddress.Id;
            }

            var addFile = await _fileServices.CreateAsync(fileMapper);

            if (addFile != null)
                return Ok(fileAddDto);

            return BadRequest("An error occurred while adding the file.");
        }




        [Authorize(Roles = AuthorizationRoles.Admin + "," + AuthorizationRoles.Responsible)]
        [HttpPut(FileRouting.Edit)]
        public async Task<IActionResult> UpdateAync(FileUpdateDto fileUpdateDto)
        {
            if (fileUpdateDto == null)
                return BadRequest("No Data For Update");

            var file = await _fileServices.Find(x => x.Slug == fileUpdateDto.Slug);


            var addressMapper = _mapper.Map<Addrees>(fileUpdateDto.address);
            addressMapper.Id = file.AddressID;

            var createdAddress = await _addressServices.UpdateAsync(addressMapper);

            if(createdAddress != null)
            {
                var fileMapper = _mapper.Map<Core.Entities.File>(fileUpdateDto);
                fileMapper.AddressID = addressMapper.Id;
                var updateFile = await _fileServices.UpateAsync(fileMapper);

                if (updateFile != null)
                    return Ok("Successed Edit ");
            }

           
            return BadRequest();
        }




        [Authorize(Roles = AuthorizationRoles.Admin + "," + AuthorizationRoles.Responsible)]
        [HttpDelete(FileRouting.Delete)]
        public async Task<IActionResult> DeleteAync(string slug)
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



