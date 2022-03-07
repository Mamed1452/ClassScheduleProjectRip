using System.Collections.Generic;
using Mohajer.ClassScheduleProject.Organizations.Dto;

namespace Mohajer.ClassScheduleProject.Web.Areas.App.Models.Common
{
    public interface IOrganizationUnitsEditViewModel
    {
        List<OrganizationUnitDto> AllOrganizationUnits { get; set; }

        List<string> MemberedOrganizationUnits { get; set; }
    }
}