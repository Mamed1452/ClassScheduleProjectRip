using Abp.Application.Services.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.MainDomains.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}