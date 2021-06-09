using Application.Common.Models.Account;
using AutoMapper;

namespace Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateAccountRequest, CreateAccountCommand>();
        }
    }
}