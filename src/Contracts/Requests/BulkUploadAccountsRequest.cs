using System.IO;

namespace Contracts.Requests
{
    public class BulkUploadAccountsRequest
    {
        public Stream UploadFile { get; set; }
    }
}
