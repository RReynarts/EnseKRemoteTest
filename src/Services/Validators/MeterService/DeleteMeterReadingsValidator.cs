using Contracts.Requests;
using FluentValidation;

namespace Services.Validators.MeterService
{
    public class DeleteMeterReadingsValidator : BaseValidator<DeleteMeterReadingRequest>
    {
        public DeleteMeterReadingsValidator()
        {
            RuleFor(a => a.MeterReaderId)
                .NotEmpty()
                .WithMessage(ValidationMessages.RequiredField);
        }
    }
}
