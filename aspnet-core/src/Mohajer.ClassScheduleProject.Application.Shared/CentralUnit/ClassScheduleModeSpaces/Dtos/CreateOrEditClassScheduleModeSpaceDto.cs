using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleModeSpaces.Dtos
{
    public class CreateOrEditClassScheduleModeSpaceDto : EntityDto<long?>
    {

        [Required]
        [StringLength(ClassScheduleModeSpaceConsts.MaxNameClassScheduleModeSpacesLength, MinimumLength = ClassScheduleModeSpaceConsts.MinNameClassScheduleModeSpacesLength)]
        public string NameClassScheduleModeSpaces { get; set; }

        public bool IsLock { get; set; }

        public long ListOfClassScheduleModeSpaceId { get; set; }

        public int UniversityProfessorId { get; set; }

        public long WorkTimeInDayId { get; set; }

        public long LessonId { get; set; }

    }
}