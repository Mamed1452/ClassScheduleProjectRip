﻿using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.Localization.Dto
{
    public class CreateOrUpdateLanguageInput
    {
        [Required]
        public ApplicationLanguageEditDto Language { get; set; }
    }
}