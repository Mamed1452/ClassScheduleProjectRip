using Abp.AutoMapper;
using Mohajer.ClassScheduleProject.Organizations.Dto;

namespace Mohajer.ClassScheduleProject.Models.Users
{
    [AutoMapFrom(typeof(OrganizationUnitDto))]
    public class OrganizationUnitModel : OrganizationUnitDto
    {
        public bool IsAssigned { get; set; }
    }
}