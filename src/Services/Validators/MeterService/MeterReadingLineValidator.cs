using FluentValidation;
using Services.Models;

namespace Services.Validators.MeterService
{
    public class MeterReadingLineValidator : BaseValidator<MeterReadingLine>
    {
        public MeterReadingLineValidator()
        {
            RuleFor(a => a.AccountId)
                .NotEmpty()
                .WithMessage(ValidationMessages.RequiredField)
                .Custom((x, context) =>
                {
                    if (!int.TryParse(x, out _))
                    {
                        context.AddFailure(string.Format(ValidationMessages.NotANumber, x));
                    }
                });
            RuleFor(a => a.MeterReadingDateTime)
                .NotEmpty()
                .WithMessage(ValidationMessages.RequiredField)
                ;
            RuleFor(a => a.MeterReadValue)
                .NotEmpty()
                .WithMessage(ValidationMessages.RequiredField)
                .Custom((x, context) =>
                {
                    if (!int.TryParse(x, out _))
                    {
                        context.AddFailure(string.Format(ValidationMessages.NotANumber, x));
                    }
                });
        }
    }
}
