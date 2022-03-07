using AutoMapper;
using Mohajer.ClassScheduleProject.Authorization.Users;
using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.Startup
{
    public static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<User, UserDto>()
                .ForMember(dto => dto.Roles, options => options.Ignore())
                .ForMember(dto => dto.OrganizationUnits, options => options.Ignore());
        }
    }
}