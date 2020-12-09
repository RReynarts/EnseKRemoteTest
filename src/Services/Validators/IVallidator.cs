using Common.ErrorHandling;

namespace Services.Validators
{
    public interface IValidator<in TCommand>
    {
        void Validate(TCommand command);
    }
}
