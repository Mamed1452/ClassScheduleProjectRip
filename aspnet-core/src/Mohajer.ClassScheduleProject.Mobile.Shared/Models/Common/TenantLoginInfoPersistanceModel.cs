using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Mohajer.ClassScheduleProject.Sessions.Dto;

namespace Mohajer.ClassScheduleProject.Models.Common
{
    [AutoMapFrom(typeof(TenantLoginInfoDto)),
     AutoMapTo(typeof(TenantLoginInfoDto))]
    public class TenantLoginInfoPersistanceModel : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}