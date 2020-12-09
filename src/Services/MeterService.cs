using System;
using System.Threading.Tasks;
using Common.Services;
using Contracts;
using Contracts.Requests;
using Contracts.Responses;

namespace Services
{
    public class MeterService : IMeterService
    {
        private readonly IRequestHandlerMediator _mediator;
        public MeterService(IRequestHandlerMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<MeterReadingResponse> GetMeterReadingAsync(MeterReadingRequest request)
        {
            return await _mediator.ExecuteAsync<MeterReadingRequest, MeterReadingResponse>(request);
        }

        public async Task<MeterReadingsResponse> GetMeterReadingsAsync(MeterReadingsRequest meterReadingsRequest)
        {
            return await _mediator.ExecuteAsync<MeterReadingsRequest, MeterReadingsResponse>(meterReadingsRequest);
        }

        public async Task<BulkUploadMeterReadingsResponse> BuklUploadMeterReadings(BulkUploadMeterReadingsRequest request)
        {
            return await _mediator.ExecuteAsync<BulkUploadMeterReadingsRequest, BulkUploadMeterReadingsResponse>(request);
        }

        public async Task<AddMeterReadingResponse> AddMeterReadingAsync(AddMeterReadingRequest meterReadingRequest)
        {
            return await _mediator.ExecuteAsync<AddMeterReadingRequest, AddMeterReadingResponse>(meterReadingRequest);
        }

        public async Task<UpdateMeterReadingResponse> UpdateMeterReadingAsync(UpdateMeterReadingRequest meterReadingRequest)
        {
            return await _mediator.ExecuteAsync<UpdateMeterReadingRequest, UpdateMeterReadingResponse>(meterReadingRequest);
        }

        public async Task<DeleteMeterReadingResponse> DeleteMeterReadingAsync(DeleteMeterReadingRequest deleteMeterReadingRequest)
        {
            return await _mediator.ExecuteAsync<DeleteMeterReadingRequest, DeleteMeterReadingResponse>(deleteMeterReadingRequest);
        }
    }
}
