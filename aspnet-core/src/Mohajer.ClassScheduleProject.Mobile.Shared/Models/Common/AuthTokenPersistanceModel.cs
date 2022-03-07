using System;
using Abp.AutoMapper;
using Mohajer.ClassScheduleProject.Sessions.Dto;

namespace Mohajer.ClassScheduleProject.Models.Common
{
    [AutoMapFrom(typeof(ApplicationInfoDto)),
     AutoMapTo(typeof(ApplicationInfoDto))]
    public class ApplicationInfoPersistanceModel
    {
        public string Version { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}