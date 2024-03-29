﻿using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleResults.Dtos
{
    public class CreateOrEditClassScheduleResultDto : EntityDto<long?>
    {

        public long WorkTimeInDayId { get; set; }

        public long LessonId { get; set; }

        public int UniversityProfessorId { get; set; }

        public long SemesterId { get; set; }

        public int GradeId { get; set; }

        public int UniversityMajorId { get; set; }

        public int UniversityDepartmentId { get; set; }

        public int ClassroomBuildingId { get; set; }

        public long ListOfAllCalculatedResultId { get; set; }

        public long ClassScheduleModeSpaceId { get; set; }

    }
}