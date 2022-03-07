using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.Editions.Dto;

namespace Mohajer.ClassScheduleProject.Web.Areas.App.Models.Common
{
    public interface IFeatureEditViewModel
    {
        List<NameValueDto> FeatureValues { get; set; }

        List<FlatFeatureDto> Features { get; set; }
    }
}