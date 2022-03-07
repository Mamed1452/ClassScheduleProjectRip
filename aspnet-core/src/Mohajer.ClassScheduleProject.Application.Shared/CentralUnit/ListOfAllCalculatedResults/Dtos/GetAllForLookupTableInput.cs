using Abp.Application.Services.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.ListOfAllCalculatedResults.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}