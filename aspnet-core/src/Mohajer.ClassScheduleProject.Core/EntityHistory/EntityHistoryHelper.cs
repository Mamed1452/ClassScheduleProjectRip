using Mohajer.ClassScheduleProject.CentralUnit.ListOfAllCalculatedResults;
using Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleModeSpaces;
using Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleResults;
using Mohajer.ClassScheduleProject.CentralUnit.LessonsOfUniversityProfessors;
using Mohajer.ClassScheduleProject.CentralUnit.LessonsOfSemesters;
using Mohajer.ClassScheduleProject.CentralUnit.Lessons;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessorWorkingTimes;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessors;
using Mohajer.ClassScheduleProject.CentralUnit.WorkTimeInDays;
using Mohajer.ClassScheduleProject.CentralUnit.AssigningGradeToUniversityMajors;
using Mohajer.ClassScheduleProject.CentralUnit.Semesters;
using Mohajer.ClassScheduleProject.CentralUnit.Grades;
using Mohajer.ClassScheduleProject.CentralUnit.AssigningUniversityMajorToClassroomBuildings;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityDepartments;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityMajors;
using Mohajer.ClassScheduleProject.CentralUnit.ClassroomBuildings;
using System;
using System.Linq;
using Abp.Organizations;
using Mohajer.ClassScheduleProject.Authorization.Roles;
using Mohajer.ClassScheduleProject.MultiTenancy;

namespace Mohajer.ClassScheduleProject.EntityHistory
{
    public static class EntityHistoryHelper
    {
        public const string EntityHistoryConfigurationName = "EntityHistory";

        public static readonly Type[] HostSideTrackedTypes =
        {
            typeof(OrganizationUnit), typeof(Role), typeof(Tenant)
        };

        public static readonly Type[] TenantSideTrackedTypes =
        {
            typeof(ListOfAllCalculatedResult),
            typeof(ClassScheduleModeSpace),
            typeof(ClassScheduleResult),
            typeof(LessonsOfUniversityProfessor),
            typeof(LessonsOfSemester),
            typeof(Lesson),
            typeof(UniversityProfessorWorkingTime),
            typeof(UniversityProfessor),
            typeof(WorkTimeInDay),
            typeof(AssigningGradeToUniversityMajor),
            typeof(Semester),
            typeof(Grade),
            typeof(AssigningUniversityMajorToClassroomBuilding),
            typeof(UniversityDepartment),
            typeof(UniversityMajor),
            typeof(ClassroomBuilding),
            typeof(OrganizationUnit), typeof(Role)
        };

        public static readonly Type[] TrackedTypes =
            HostSideTrackedTypes
                .Concat(TenantSideTrackedTypes)
                .GroupBy(type => type.FullName)
                .Select(types => types.First())
                .ToArray();
    }
}