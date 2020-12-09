using System;

namespace Contracts.Requests
{
    public class AddMeterReadingRequest
    {
        public int AccountId { get; set; }
        public DateTime MeterReadingDateTime { get; set; }
        public int MeterReadValue { get; set; }
    }
}
