using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessorWorkingTimes.Dtos
{
    public class CreateGroupInputDto
    {
        [Required]
        public int UniversityProfessorId { get; set; }

        [Required]
        public long WorkTimeInDayIdMin { get; set; }

        [Required]
        public long WorkTimeInDayIdMax { get; set; }
    }
}
