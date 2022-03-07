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