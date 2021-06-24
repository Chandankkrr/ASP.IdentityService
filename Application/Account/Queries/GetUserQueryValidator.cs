using FluentValidation;

namespace Application.Account.Queries
{
    public class GetUserQueryValidator: AbstractValidator<GetUserQuery>
    {
        public GetUserQueryValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty();
        }
    }
}