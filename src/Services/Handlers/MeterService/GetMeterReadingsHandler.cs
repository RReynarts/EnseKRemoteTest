using System;
using System.Linq;
using System.Threading.Tasks;
using Common.Repository;
using Common.Services;
using Contracts.Requests;
using Contracts.Responses;
using Domain;
using Services.Validators.MeterService;

namespace Services.Handlers.MeterService
{
    public class GetMeterReadingsHandler : QueryRequestHandler<MeterReadingsRequest, MeterReadingsResponse>
    {
        private readonly IDataSetFactory _repository;
        private readonly GetMeterReadingsValidator _validator;

        public GetMeterReadingsHandler(IUnitOfWork context, IDataSetFactory repository, GetMeterReadingsValidator validator) :
            base(context)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        protected override async Task<MeterReadingsResponse> QueryAsync(MeterReadingsRequest request)
        {
            _validator.Validate(request);

            var meterReadings = _repository.Resolve<MeterReading>()
                .Where(a => a.AccountId == request.AccountId).Select(s => new MeterReadingResponse
                {
                    MeterReadingId = s.MeterReadingId,
                    AccountId = s.AccountId,
                    MeterReadValue = s.MeterReadValue,
                    MeterReadingDateTime = s.MeterReadingDateTime
                }).ToList();
            
            return new MeterReadingsResponse
            {
                MeterReadings = meterReadings
            };
        }
    }
}