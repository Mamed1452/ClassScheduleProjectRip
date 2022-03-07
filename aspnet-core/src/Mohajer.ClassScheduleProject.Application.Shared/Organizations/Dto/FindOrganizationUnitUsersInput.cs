using Mohajer.ClassScheduleProject.Dto;

namespace Mohajer.ClassScheduleProject.Organizations.Dto
{
    public class FindOrganizationUnitUsersInput : PagedAndFilteredInputDto
    {
        public long OrganizationUnitId { get; set; }
    }
}
