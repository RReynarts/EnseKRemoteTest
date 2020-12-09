using Contracts.Requests;
using FluentValidation;

namespace Services.Validators.MeterService
{
    public class BuklUploadMeterReadingsValidator : BaseValidator<BulkUploadMeterReadingsRequest>
    {
        public BuklUploadMeterReadingsValidator()
        {
            RuleFor(a => a.UploadFile)
                .NotEmpty()
                .WithMessage(ValidationMessages.RequiredField);
        }
    }
}
