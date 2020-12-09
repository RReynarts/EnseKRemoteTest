using Contracts.Requests;
using FluentValidation;

namespace Services.Validators.MeterService
{
    public class UpdateMeterReadingsValidator : BaseValidator<UpdateMeterReadingRequest>
    {
        public UpdateMeterReadingsValidator()
        {
            RuleFor(a => a.MeterReadingId)
                .NotEmpty()
                .WithMessage(ValidationMessages.RequiredField);
            RuleFor(a => a.AccountId)
                .NotEmpty()
                .WithMessage(ValidationMessages.RequiredField);
            RuleFor(a => a.MeterReadingDateTime)
                .NotEmpty()
                .WithMessage(ValidationMessages.RequiredField);
            RuleFor(a => a.MeterReadValue)
                .NotEmpty()
                .WithMessage(ValidationMessages.RequiredField);
        }
    }
}
