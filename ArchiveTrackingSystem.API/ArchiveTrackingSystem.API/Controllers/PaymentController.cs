using ArchiveTrackingSystem.Core.Constant;
using ArchiveTrackingSystem.Core.Dto.ActiveDtos;
using ArchiveTrackingSystem.Core.Dto.PaymentDtos;
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

    public class PaymentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly PaymentServices _paymentServices;
        public PaymentController(IMapper mapper, PaymentServices paymentServices)
        {
            _mapper=mapper;
            _paymentServices=paymentServices;
        }


        [HttpGet(PaymentRouting.GetBySlug)]
        public async Task<IActionResult> GetAsync(string slug)
        {
            var pay = await _paymentServices.Find(x => x.Slug == slug);

            var payMapper = _mapper.Map<PaymentGetDto>(pay);
            if (payMapper != null)
                return Ok(payMapper);



            return NotFound();
        }

        [HttpGet(PaymentRouting.List)]
        public async Task<IActionResult> GetListAsync()
        {
            var pays = await _paymentServices.GetListAsync();

            var paysMapper = _mapper.Map<IEnumerable<PaymentGetDto>>(pays);
            if (paysMapper != null)
                return Ok(paysMapper);



            return NotFound();
        }

        [HttpPost(PaymentRouting.Create)]
        public async Task<IActionResult> CreateAsync(PaymentAddDto paymentAddDto)
        {
            if (paymentAddDto == null) return BadRequest("No Data For Add");

            var pyment = await _paymentServices.Find(x => x.Name == paymentAddDto.Name);
            if (pyment != null)
                return BadRequest($"The name is {paymentAddDto.Name}already used");

            var payMapper = _mapper.Map<Payment>(paymentAddDto);

            var addPay = await _paymentServices.CreateAsync(payMapper);
            if (addPay != null)
                return Ok(addPay);

            return BadRequest("There was an error happend");
        }

        [HttpPut(PaymentRouting.Edit)]
        public async Task<IActionResult> UpdateAsync(PaymentEditDto paymentEditDto)
        {
            if (paymentEditDto == null) return BadRequest("No Data For Edit");

            var payMapper = _mapper.Map<Payment>(paymentEditDto);
            var editPay = await _paymentServices.UpdateAsync(payMapper);
            if (editPay != null) return Ok(editPay);

            return BadRequest("There was an error happend");
        }

        [HttpDelete(PaymentRouting.Delete)]
        public async Task<IActionResult> DeleteAsync(string slug)
        {
            var payment = await _paymentServices.Find(x => x.Slug == slug);
            if (payment != null)
            {
                var deleteActive = await _paymentServices.DeleteAsync(payment);

                if (deleteActive == "Successed Deleted")
                    return Ok(payment);


            }

            return BadRequest("It Can Not Delete Null Active");

        }
    }
}
