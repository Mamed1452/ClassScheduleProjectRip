using System;
using Abp.Application.Services.Dto;

namespace Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleResults.Dtos
{
    public class ClassScheduleResultDto : EntityDto<long>
    {
        public string WorkTimeInDayId { get; set; }

        public string LessonId { get; set; }

        public string UniversityProfessorId { get; set; }

        public string SemesterId { get; set; }

        public string GradeId { get; set; }

        public string UniversityMajorId { get; set; }

        public string UniversityDepartmentId { get; set; }

        public string ClassroomBuildingId { get; set; }

        public long ListOfAllCalculatedResultId { get; set; }

        public long ClassScheduleModeSpaceId { get; set; }

    }
}