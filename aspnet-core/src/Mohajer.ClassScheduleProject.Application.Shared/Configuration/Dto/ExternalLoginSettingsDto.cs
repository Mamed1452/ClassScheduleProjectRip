﻿using System.Collections.Generic;

namespace Mohajer.ClassScheduleProject.Configuration.Dto
{
    public class ExternalLoginSettingsDto
    {
        public List<string> EnabledSocialLoginSettings { get; set; } = new List<string>();
    }
}