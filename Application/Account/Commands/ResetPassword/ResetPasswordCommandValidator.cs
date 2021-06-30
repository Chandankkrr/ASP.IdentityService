using FluentValidation;

namespace Application.Account.Commands.ResetPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

             RuleFor(x => x.NewPassword)
                .NotEmpty()
                .Equal(x => x.ConfirmNewPassword)
                .WithMessage("Passwords do not match");

            RuleFor(x => x.ConfirmNewPassword)
                .NotEmpty()
                .Equal(x => x.NewPassword)
                .WithMessage("Passwords do not match");

            RuleFor(x => x.Token)
                .NotEmpty();
        }
    }
}
