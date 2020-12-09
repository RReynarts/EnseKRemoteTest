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
        AddMeterReadingHandler : CommandRequestHandler<AddMeterReadingRequest,
            AddMeterReadingResponse>
    {
        private readonly IDataSetFactory _repository;
        private readonly AddMeterReadingsValidator _validator;

        public AddMeterReadingHandler(IUnitOfWork context, IDataSetFactory repository,
            AddMeterReadingsValidator validator) :
            base(context)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        protected override async Task<AddMeterReadingResponse> Execute(
            AddMeterReadingRequest request)
        {
            _validator.Validate(request);

            var accountExists = await _repository.Resolve<Account>()
                .AnyAsync(a => a.AccountId == request.AccountId);
            if (!accountExists)
            {
                throw new ValidationException(string.Format(ValidationMessages.AccountNotFound, request.AccountId));
            }

            _repository.Resolve<MeterReading>().Add(new MeterReading
            {
                AccountId = request.AccountId,
                MeterReadValue = request.MeterReadValue,
                MeterReadingDateTime = request.MeterReadingDateTime
            });

            return new AddMeterReadingResponse();
        }
    }
}