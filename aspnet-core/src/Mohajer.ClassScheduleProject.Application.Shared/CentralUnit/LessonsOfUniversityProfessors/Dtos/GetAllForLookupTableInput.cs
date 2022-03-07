using Abp.Application.Services.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.LessonsOfUniversityProfessors.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}