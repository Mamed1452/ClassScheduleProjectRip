using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Mohajer.ClassScheduleProject.CentralUnit.LessonsOfSemesters.Dtos
{
    public class GetLessonsOfSemesterForEditOutput
    {
        public CreateOrEditLessonsOfSemesterDto LessonsOfSemester { get; set; }

        public string LessonNameLesson { get; set; }

        public string SemesterSemesterName { get; set; }

    }
}