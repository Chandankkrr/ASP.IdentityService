using Application.Account.Commands.ChangePassword;
using Application.Account.Commands.CreateAccount;
using Application.Account.Commands.ForgotPassword;
using Application.Account.Commands.ResetPassword;
using Application.Account.Queries;
using Application.Common.Models.Account;
using Application.Common.Models.Login;
using Application.Login.Commands;
using AutoMapper;
using Contracts.Account.Requests;
using Contracts.Account.Responses;
using Contracts.Login.Requests;
using Contracts.Login.Responses;

namespace Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Account
            CreateMap<CreateAccountRequest, CreateAccountCommand>();
            CreateMap<ApplicationUserResult, CreateAccountResponse>();
            
            CreateMap<GetUserRequest, GetUserQuery>();
            CreateMap<ApplicationUserResult, GetUserResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.User.Id))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));

            CreateMap<ChangePasswordRequest, ChangePasswordCommand>()
                .ForMember(dest => dest.Token, opt => opt.Ignore());
            CreateMap<ChangePasswordCommandResult, ChangePasswordResponse>();

            CreateMap<ResetPasswordRequest, ResetPasswordCommand>();
            CreateMap<ResetPasswordCommandResult, ResetPasswordResponse>();

            CreateMap<ForgotPasswordRequest, ForgotPasswordCommand>();

            // Login
            CreateMap<LoginRequest, LoginCommand>();
            CreateMap<LoginCommandResult, LoginResponse>();
        }
    }
}