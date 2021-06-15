using Application.Common.Mappings;
using AutoMapper;
using Xunit;

namespace Application.Tests.Unit.Mapping
{
    public class MappingProfileTests
    {
        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            config.AssertConfigurationIsValid();
        }
    }
}