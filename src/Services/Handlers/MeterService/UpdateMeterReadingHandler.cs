using System;
using System.Threading.Tasks;
using Common.ErrorHandling;
using Common.Repository;
using Common.Services;
using Contracts.Requests;
using Contracts.Responses;
using Domain;
using Services.Validators;
using Services.Validators.MeterService;

namespace Services.Handlers.MeterService
{
    public class
        UpdateMeterReadingHandler : CommandRequestHandler<UpdateMeterReadingRequest,
            UpdateMeterReadingResponse>
    {
        private readonly IDataSetFactory _repository;
        private readonly UpdateMeterReadingsValidator _validator;

        public UpdateMeterReadingHandler(IUnitOfWork context, IDataSetFactory repository,
            UpdateMeterReadingsValidator validator) :
            base(context)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        protected override async Task<UpdateMeterReadingResponse> Execute(
            UpdateMeterReadingRequest request)
        {
            _validator.Validate(request);

            var meterReading = await _repository.Resolve<MeterReading>()
                .FirstOrDefaultAsync(r => r.MeterReadingId == request.MeterReadingId);
            if (meterReading == null)
            {
                throw new ValidationException(string.Format(ValidationMessages.MeterReadingNotFound, request.MeterReadingId));
            }

            var accountExists = await _repository.Resolve<Account>()
                .AnyAsync(a => a.AccountId == request.AccountId);
            if (!accountExists)
            {
                throw new ValidationException(string.Format(ValidationMessages.AccountNotFound, request.AccountId));
            }

            meterReading.AccountId = request.AccountId;
            meterReading.MeterReadValue = request.MeterReadValue;
            meterReading.MeterReadingDateTime = request.MeterReadingDateTime;

            return new UpdateMeterReadingResponse();
        }
    }
}