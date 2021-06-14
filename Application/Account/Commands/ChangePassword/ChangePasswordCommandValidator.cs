using FluentValidation;

namespace Application.Account.Commands.ChangePassword
{
    public class ChangePasswordCommandValidator: AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty();

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .Equal(x => x.ConfirmNewPassword)
                .WithMessage("Passwords do not match");

            RuleFor(x => x.ConfirmNewPassword)
                .NotEmpty()
                .Equal(x => x.NewPassword)
                .WithMessage("Passwords do not match");
        }
    }
}