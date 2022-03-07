using System.ComponentModel.DataAnnotations;
using Abp.Localization;

namespace Mohajer.ClassScheduleProject.Localization.Dto
{
    public class SetDefaultLanguageInput
    {
        [Required]
        [StringLength(ApplicationLanguage.MaxNameLength)]
        public virtual string Name { get; set; }
    }
}