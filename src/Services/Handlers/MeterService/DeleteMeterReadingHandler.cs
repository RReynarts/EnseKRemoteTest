using System;
using System.Threading.Tasks;
using Common.Repository;
using Common.Services;
using Contracts.Requests;
using Contracts.Responses;
using Domain;
using Services.Validators.MeterService;

namespace Services.Handlers.MeterService
{
    public class
        DeleteMeterReadingHandler : CommandRequestHandler<DeleteMeterReadingRequest,
            DeleteMeterReadingResponse>
    {
        private readonly IDataSetFactory _repository;
        private readonly DeleteMeterReadingsValidator _validator;

        public DeleteMeterReadingHandler(IUnitOfWork context, IDataSetFactory repository,
            DeleteMeterReadingsValidator validator) :
            base(context)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        protected override async Task<DeleteMeterReadingResponse> Execute(
            DeleteMeterReadingRequest request)
        {
            _validator.Validate(request);

            var meterReading = await _repository.Resolve<MeterReading>().FirstOrDefaultAsync(r => r.MeterReadingId == request.MeterReaderId);
            if (meterReading != null)
            {
                _repository.Resolve<MeterReading>().Remove(meterReading);
            }
            
            return new DeleteMeterReadingResponse();
        }
    }
}