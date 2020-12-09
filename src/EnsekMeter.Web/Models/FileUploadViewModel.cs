using Microsoft.AspNetCore.Http;

namespace EnsekMeter.Web.Models
{
    public class FileUploadViewModel
    {
        public BulkUploadMeterReadingsViewModel Result { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
