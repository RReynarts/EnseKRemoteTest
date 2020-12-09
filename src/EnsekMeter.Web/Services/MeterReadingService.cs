using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EnsekMeter.Web.Models;
using Newtonsoft.Json;

namespace EnsekMeter.Web.Services
{
    public class MeterReadingService : IMeterReadingService
    {
        private readonly HttpClient _httpClient;

        public MeterReadingService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<List<MeterReadingViewModel>> GetMeterReadings(int accountId)
        {
            var response = await _httpClient.GetStringAsync($"https://localhost:44377/api/Account/{accountId}/MeterReadings");
            var meterReadings = JsonConvert.DeserializeObject<List<MeterReadingViewModel>>(response);
            return meterReadings;
        }

        public async Task<MeterReadingViewModel> GetMeterReading(int meterReadingId)
        {
            var response = await _httpClient.GetStringAsync($"https://localhost:44377/api/Meters/{meterReadingId}");
            var meterReadings = JsonConvert.DeserializeObject<MeterReadingViewModel>(response);
            return meterReadings;
        }

        public async Task UpdateMeterReading(MeterReadingViewModel meterReading)
        {
            await _httpClient.PutAsync("https://localhost:44377/api/Meters", new StringContent(JsonConvert.SerializeObject(meterReading), Encoding.UTF8, "application/json"));
        }

        public async Task AddMeterReading(MeterReadingViewModel meterReading)
        {
            await _httpClient.PostAsync("https://localhost:44377/api/Meters", new StringContent(JsonConvert.SerializeObject(meterReading), Encoding.UTF8, "application/json"));
        }

        public async Task DeleteMeterReading(int meterReadingId)
        {
            await _httpClient.DeleteAsync($"https://localhost:44377/api/Meters/{meterReadingId}");
        }

        public async Task<BulkUploadMeterReadingsViewModel> BulkUploadMeterReadings(FileUploadViewModel fileUpload)
        {
            await using var ms = new MemoryStream();
            await fileUpload.FormFile.CopyToAsync(ms);
            ms.Position = 0;

            var multipartContent = new MultipartFormDataContent
            {
                {new ByteArrayContent(ms.ToArray()), "file", fileUpload.FormFile.FileName}
            };
            var result = await _httpClient.PostAsync("https://localhost:44377/meter-reading-uploads", multipartContent);
            if (result.IsSuccessStatusCode)
            {
                return
                    JsonConvert.DeserializeObject<BulkUploadMeterReadingsViewModel>(
                        await result.Content.ReadAsStringAsync());
            }

            return null;
        }
    }
}