using System;
using System.IO;
using System.Threading.Tasks;
using Common.ErrorHandling;
using Contracts;
using Contracts.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnsekMeter.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class MetersController : ControllerBase
    {
        private readonly IMeterService _meterService;

        public MetersController(IMeterService meterService)
        {
            _meterService = meterService ?? throw new ArgumentNullException(nameof(meterService));
        }

        [HttpGet]
        [Route("{meterReadingId}")]
        public async Task<IActionResult> GetMeterReading(int meterReadingId)
        {
            var result = await _meterService.GetMeterReadingAsync(new MeterReadingRequest { MeterReadingId = meterReadingId });
            return Ok(result);
        }

        [HttpGet]
        [Route("/api/Account/{accountId}/MeterReadings")]
        public async Task<IActionResult> GetMeterReadings(int accountId)
        {
            var result = await _meterService.GetMeterReadingsAsync(new MeterReadingsRequest {AccountId = accountId});
            return Ok(result.MeterReadings);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddMeterReadings([FromBody]AddMeterReadingRequest meterReadingRequest)
        {
            var result = await _meterService.AddMeterReadingAsync(meterReadingRequest);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{meterReadingId}")]
        public async Task<IActionResult> DeleteMeterReadings(int meterReadingId)
        {
            var result = await _meterService.DeleteMeterReadingAsync(new DeleteMeterReadingRequest{MeterReaderId = meterReadingId});
            return Ok(result);
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateMeterReadings([FromBody]UpdateMeterReadingRequest meterReadingRequest)
        {
            var result = await _meterService.UpdateMeterReadingAsync(meterReadingRequest);
            return Ok(result);
        }

        [HttpPost]
        [Route("/meter-reading-uploads")]
        public async Task<IActionResult> BulkUpload(IFormFile file)
        {
            if (file == null) throw new ValidationException("File is null");
            if (file.Length == 0) throw new ValidationException("File is empty");
            var request = new BulkUploadMeterReadingsRequest{UploadFile = new MemoryStream()};
            await file.CopyToAsync(request.UploadFile);
            var result = await _meterService.BuklUploadMeterReadings(request);
            return Ok(result);
        }
    }
}
