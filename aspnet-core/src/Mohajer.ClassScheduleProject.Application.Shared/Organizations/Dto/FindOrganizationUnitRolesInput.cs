using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.Organizations.Dto
{
    public class FindOrganizationUnitRolesInput : PagedAndFilteredInputDto
    {
        public long OrganizationUnitId { get; set; }
    }
}