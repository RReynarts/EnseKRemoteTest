using System.Collections.Generic;

namespace Contracts.Responses
{
    public class BulkUploadAccountsResponse
    {
        public int SuccesfulUploads { get; set; }
        public int FailedUploads { get; set; }
        public List<string> UploadErrors { get; set; }
    }
}
