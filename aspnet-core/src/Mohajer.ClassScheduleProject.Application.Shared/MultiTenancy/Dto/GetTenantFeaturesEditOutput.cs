using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Mohajer.ClassScheduleProject.Editions.Dto;

namespace Mohajer.ClassScheduleProject.MultiTenancy.Dto
{
    public class GetTenantFeaturesEditOutput
    {
        public List<NameValueDto> FeatureValues { get; set; }

        public List<FlatFeatureDto> Features { get; set; }
    }
}