using System.Collections.Generic;
using Abp.Localization;
using Mohajer.ClassScheduleProject.Install.Dto;

namespace Mohajer.ClassScheduleProject.Web.Models.Install
{
    public class InstallViewModel
    {
        public List<ApplicationLanguage> Languages { get; set; }

        public AppSettingsJsonDto AppSettingsJson { get; set; }
    }
}
