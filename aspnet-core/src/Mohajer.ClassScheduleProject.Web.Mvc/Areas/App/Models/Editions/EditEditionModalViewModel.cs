using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Mohajer.ClassScheduleProject.Editions.Dto;
using Mohajer.ClassScheduleProject.Web.Areas.App.Models.Common;

namespace Mohajer.ClassScheduleProject.Web.Areas.App.Models.Editions
{
    [AutoMapFrom(typeof(GetEditionEditOutput))]
    public class EditEditionModalViewModel : GetEditionEditOutput, IFeatureEditViewModel
    {
        public bool IsEditMode => Edition.Id.HasValue;

        public IReadOnlyList<ComboboxItemDto> EditionItems { get; set; }

        public IReadOnlyList<ComboboxItemDto> FreeEditionItems { get; set; }
    }
}