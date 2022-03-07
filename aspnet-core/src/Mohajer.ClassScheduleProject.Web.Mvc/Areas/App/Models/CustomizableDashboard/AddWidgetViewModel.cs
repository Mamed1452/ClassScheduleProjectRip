using System.Collections.Generic;
using Mohajer.ClassScheduleProject.DashboardCustomization.Dto;

namespace Mohajer.ClassScheduleProject.Web.Areas.App.Models.CustomizableDashboard
{
    public class AddWidgetViewModel
    {
        public List<WidgetOutput> Widgets { get; set; }

        public string DashboardName { get; set; }

        public string PageId { get; set; }
    }
}
