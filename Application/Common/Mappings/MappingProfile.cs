using Application.Account.Commands.CreateAccount;
using Application.Common.Models.Account;
using Application.Common.Models.Login;
using Application.Login.Commands;
using AutoMapper;
using Contracts.Requests.Account;
using Contracts.Requests.Login;
using Contracts.Responses.Account;

namespace Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Account
            CreateMap<CreateAccountRequest, CreateAccountCommand>();
            CreateMap<ApplicationUserResult, CreateAccountResponse>();

            // Login
            CreateMap<LoginRequest, LoginCommand>();
            CreateMap<LoginResult, LoginResponse>();
        }
    }
}