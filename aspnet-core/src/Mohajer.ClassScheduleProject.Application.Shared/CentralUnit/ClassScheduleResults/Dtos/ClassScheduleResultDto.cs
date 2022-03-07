using System;
using Abp.Application.Services.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleResults.Dtos
{
    public class ClassScheduleResultDto : EntityDto<long>
    {
        public long WorkTimeInDayId { get; set; }

        public int LessonId { get; set; }

        public int UniversityProfessorId { get; set; }

        public long SemesterId { get; set; }

        public int GradeId { get; set; }

        public int UniversityMajorId { get; set; }

        public int UniversityDepartmentId { get; set; }

        public long ListOfAllCalculatedResultId { get; set; }

        public long ClassScheduleModeSpaceId { get; set; }

    }
}