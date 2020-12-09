using System.Collections.Generic;
using System.Threading.Tasks;
using EnsekMeter.Web.Models;

namespace EnsekMeter.Web.Services
{
    public interface IMeterReadingService
    {
        Task<List<MeterReadingViewModel>> GetMeterReadings(int accountId);
        Task<MeterReadingViewModel> GetMeterReading(int meterReadingId);
        Task UpdateMeterReading(MeterReadingViewModel meterReading);
        Task AddMeterReading(MeterReadingViewModel meterReading);
        Task DeleteMeterReading(int meterReadingId);
        Task<BulkUploadMeterReadingsViewModel> BulkUploadMeterReadings(FileUploadViewModel fileUpload);
    }
}
