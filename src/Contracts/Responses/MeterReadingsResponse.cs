using System.Collections.Generic;

namespace Contracts.Responses
{
    public class MeterReadingsResponse
    {
        public List<MeterReadingResponse> MeterReadings { get; set; }
    }
}
