using Application.Account.Commands.VerifyEmail;
using FluentValidation;

namespace Application.Account.Commands.ResetPassword
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
