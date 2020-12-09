using System.Threading.Tasks;
using Contracts.Requests;
using Contracts.Responses;

namespace Contracts
{
    public interface IMeterService
    {
        Task<MeterReadingResponse> GetMeterReadingAsync(MeterReadingRequest request);
        Task<MeterReadingsResponse> GetMeterReadingsAsync(MeterReadingsRequest meterReadingsRequest);
        Task<BulkUploadMeterReadingsResponse> BuklUploadMeterReadings(BulkUploadMeterReadingsRequest request);
        Task<AddMeterReadingResponse> AddMeterReadingAsync(AddMeterReadingRequest meterReadingRequest);
        Task<UpdateMeterReadingResponse> UpdateMeterReadingAsync(UpdateMeterReadingRequest meterReadingRequest);
        Task<DeleteMeterReadingResponse> DeleteMeterReadingAsync(DeleteMeterReadingRequest deleteMeterReadingRequest);
    }
}
