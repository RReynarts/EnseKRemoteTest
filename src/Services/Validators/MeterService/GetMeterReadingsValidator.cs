using Contracts.Requests;
using FluentValidation;

namespace Services.Validators.MeterService
{
    public class GetMeterReadingsValidator : BaseValidator<MeterReadingsRequest>
    {
        public GetMeterReadingsValidator()
        {
            RuleFor(a => a.AccountId)
                .NotEmpty()
                .WithMessage(ValidationMessages.RequiredField);
        }
    }
}
