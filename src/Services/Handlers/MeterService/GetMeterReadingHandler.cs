using System;
using System.Threading.Tasks;
using Common.ErrorHandling;
using Common.Repository;
using Common.Services;
using Contracts.Requests;
using Contracts.Responses;
using Domain;
using Services.Validators.MeterService;

namespace Services.Handlers.MeterService
{
    public class GetMeterReadingHandler : QueryRequestHandler<MeterReadingRequest, MeterReadingResponse>
    {
        private readonly IDataSetFactory _repository;
        private readonly GetMeterReadingValidator _validator;

        public GetMeterReadingHandler(IUnitOfWork context, IDataSetFactory repository, GetMeterReadingValidator validator) :
            base(context)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        protected override async Task<MeterReadingResponse> QueryAsync(MeterReadingRequest request)
        {
            _validator.Validate(request);
            var meterReading = await _repository.Resolve<MeterReading>()
                .FirstOrDefaultAsync(a => a.MeterReadingId == request.MeterReadingId);
            if (meterReading == null)
            {
                throw new ValidationException($"Meter reading with meterReadingId {request.MeterReadingId} not found!");
            }

            return new MeterReadingResponse
            {
                MeterReadingId = meterReading.MeterReadingId, 
                AccountId = meterReading.AccountId,
                MeterReadValue = meterReading.MeterReadValue, 
                MeterReadingDateTime = meterReading.MeterReadingDateTime
            };
        }
    }
}