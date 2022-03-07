using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.ListOfAllCalculatedResults.Dtos
{
    public class CreateOrEditListOfAllCalculatedResultDto : EntityDto<long?>
    {

        [Required]
        [StringLength(ListOfAllCalculatedResultConsts.MaxNameCalculatedResultLength, MinimumLength = ListOfAllCalculatedResultConsts.MinNameCalculatedResultLength)]
        public string NameCalculatedResult { get; set; }

        [Range(ListOfAllCalculatedResultConsts.MinPriceValue, ListOfAllCalculatedResultConsts.MaxPriceValue)]
        public int Price { get; set; }

    }
}