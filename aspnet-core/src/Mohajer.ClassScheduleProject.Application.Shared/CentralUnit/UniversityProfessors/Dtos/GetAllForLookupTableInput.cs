using Abp.Application.Services.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessors.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}