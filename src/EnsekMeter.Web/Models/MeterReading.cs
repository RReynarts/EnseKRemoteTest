using System;

namespace EnsekMeter.Web.Models
{
    public class MeterReadingViewModel
    {
        public int MeterReadingId { get; set; }
        public int AccountId { get; set; }
        public DateTime MeterReadingDateTime { get; set; }
        public int MeterReadValue { get; set; }
    }
}
