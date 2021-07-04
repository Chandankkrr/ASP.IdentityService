using FluentValidation;

namespace Application.Account.Commands.VerifyEmail
{
    public class VerifyEmailCommandValidator : AbstractValidator<VerifyEmailCommand>
    {
        public VerifyEmailCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Token)
                .NotEmpty();
        }
    }
}
