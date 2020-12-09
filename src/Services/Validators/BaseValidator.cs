using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using ValidationException = Common.ErrorHandling.ValidationException;

namespace Services.Validators
{
    public abstract class BaseValidator<T> : AbstractValidator<T>,
        IValidator<T>
    {
        public new void Validate(T command)
        {
            var result = base.Validate(command);
            var errors = new List<ValidationException>();
            if (result.IsValid) return;
            errors.AddRange(result.Errors.Select(error => new ValidationException(error.ErrorMessage)));
            throw errors.Count == 1 ? errors.First() : new ValidationException(errors.ToArray());
        }
    }
}