using System.Collections.Generic;

namespace Contracts.Responses
{
    public class BulkUploadMeterReadingsResponse
    {
        public int SuccesfulUploads { get; set; }
        public int FailedUploads { get; set; }
        public List<string> UploadErrors { get; set; }
    }
}
