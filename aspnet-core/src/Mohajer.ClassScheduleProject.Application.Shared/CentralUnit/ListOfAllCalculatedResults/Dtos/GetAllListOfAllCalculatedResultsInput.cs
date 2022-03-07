using Abp.Application.Services.Dto;
using System;

namespace Mohajer.ClassScheduleProject.CentralUnit.ListOfAllCalculatedResults.Dtos
{
    public class GetAllListOfAllCalculatedResultsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string NameCalculatedResultFilter { get; set; }

        public int? MaxPriceFilter { get; set; }
        public int? MinPriceFilter { get; set; }

    }
}