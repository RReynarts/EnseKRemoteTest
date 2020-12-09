using System.IO;

namespace Contracts.Requests
{
    public class BulkUploadMeterReadingsRequest
    {
        public Stream UploadFile { get; set; }
    }
}
