using Abp.Application.Services.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.Lessons.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}