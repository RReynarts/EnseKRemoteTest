using System.Collections.Generic;
using Common.Repository;

namespace Domain
{
    public class Account : IEntity
    {
        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<MeterReading> MeterReadings { get; set; }
    }
}
