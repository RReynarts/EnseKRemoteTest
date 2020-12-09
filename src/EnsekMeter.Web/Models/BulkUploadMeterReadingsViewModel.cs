using System.Collections.Generic;

namespace EnsekMeter.Web.Models
{
    public class BulkUploadMeterReadingsViewModel
    {
        public int SuccesfulUploads { get; set; }
        public int FailedUploads { get; set; }
        public List<string> UploadErrors { get; set; }
    }
}
