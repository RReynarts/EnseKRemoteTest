using Contracts.Requests;
using FluentValidation;

namespace Services.Validators.MeterService
{
    public class GetMeterReadingValidator : BaseValidator<MeterReadingRequest>
    {
        public GetMeterReadingValidator()
        {
            RuleFor(a => a.MeterReadingId)
                .NotEmpty()
                .WithMessage(ValidationMessages.RequiredField);
        }
    }
}
