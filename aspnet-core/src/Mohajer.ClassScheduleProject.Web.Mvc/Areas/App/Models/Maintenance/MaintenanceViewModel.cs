using System.Collections.Generic;
using Mohajer.ClassScheduleProject.Caching.Dto;

namespace Mohajer.ClassScheduleProject.Web.Areas.App.Models.Maintenance
{
    public class MaintenanceViewModel
    {
        public IReadOnlyList<CacheDto> Caches { get; set; }
    }
}