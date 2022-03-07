using System.Collections.Generic;
using Mohajer.ClassScheduleProject.DynamicEntityProperties.Dto;

namespace Mohajer.ClassScheduleProject.Web.Areas.App.Models.DynamicProperty
{
    public class CreateOrEditDynamicPropertyViewModel
    {
        public DynamicPropertyDto DynamicPropertyDto { get; set; }

        public List<string> AllowedInputTypes { get; set; }
    }
}
