using System.Collections.Generic;
using Mohajer.ClassScheduleProject.Editions.Dto;

namespace Mohajer.ClassScheduleProject.Web.Areas.App.Models.Tenants
{
    public class TenantIndexViewModel
    {
        public List<SubscribableEditionComboboxItemDto> EditionItems { get; set; }
    }
}