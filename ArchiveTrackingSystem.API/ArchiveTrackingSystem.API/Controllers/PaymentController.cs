using ArchiveTrackingSystem.Core.Dto.ActiveDtos;
using ArchiveTrackingSystem.Core.Dto.PaymentDtos;
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
    public class PaymentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly PaymentServices _paymentServices;
        public PaymentController(IMapper mapper, PaymentServices paymentServices)
        {
            _mapper=mapper;
            _paymentServices=paymentServices;
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
        public async Task<IActionResult> Create(PaymentAddDto paymentAddDto)
        {
            if (paymentAddDto == null) return BadRequest("No Data For Add");

            var payMapper = _mapper.Map<TypePayment>(paymentAddDto);
            var addPay = await _paymentServices.CreateAsync(payMapper);
            if (addPay != null) return Ok(addPay);

            return BadRequest("There was an error happend");
        }


        [HttpPut(PaymentRouting.Edit)]
        public async Task<IActionResult> Update(PaymentEditDto paymentEditDto)
        {
            if (paymentEditDto == null) return BadRequest("No Data For Edit");

            var payMapper = _mapper.Map<TypePayment>(paymentEditDto);
            var editPay = await _paymentServices.UpdateAsync(payMapper);
            if (editPay != null) return Ok(editPay);

            return BadRequest("There was an error happend");
        }

        [HttpDelete(PaymentRouting.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var payment = await _paymentServices.Find(x => x.Id == id);
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
