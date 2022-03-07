using System;
using System.Collections.Generic;
using System.Text;

namespace Mohajer.ClassScheduleProject.Configuration.Tenants.Dto
{
    public class CrmSettingsEditDto
    {
        public int ValueAddedPercentage { get; set; }
        public int StartLetterNumber { get; set; }
        public int ContractRegistrationNumber { get; set; }
        public int DefaultWarning { get; set; }
        public int DefaultCritical { get; set; }
        public int DefaultFatalDay { get; set; }
        public int StartFactorNumber { get; set; }
    }
}
