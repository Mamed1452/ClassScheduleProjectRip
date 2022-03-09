using Abp.Authorization;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Mohajer.ClassScheduleProject.Authorization
{
    /// <summary>
    /// Application's authorization provider.
    /// Defines permissions for the application.
    /// See <see cref="AppPermissions"/> for all permission names.
    /// </summary>
    public class AppAuthorizationProvider : AuthorizationProvider
    {
        private readonly bool _isMultiTenancyEnabled;

        public AppAuthorizationProvider(bool isMultiTenancyEnabled)
        {
            _isMultiTenancyEnabled = isMultiTenancyEnabled;
        }

        public AppAuthorizationProvider(IMultiTenancyConfig multiTenancyConfig)
        {
            _isMultiTenancyEnabled = multiTenancyConfig.IsEnabled;
        }

        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //COMMON PERMISSIONS (FOR BOTH OF TENANTS AND HOST)

            var pages = context.GetPermissionOrNull(AppPermissions.Pages) ?? context.CreatePermission(AppPermissions.Pages, L("Pages"));

            var listOfMainDomains = pages.CreateChildPermission(AppPermissions.Pages_ListOfMainDomains, L("ListOfMainDomains"), multiTenancySides: MultiTenancySides.Tenant);
            listOfMainDomains.CreateChildPermission(AppPermissions.Pages_ListOfMainDomains_Create, L("CreateNewListOfMainDomain"), multiTenancySides: MultiTenancySides.Tenant);
            listOfMainDomains.CreateChildPermission(AppPermissions.Pages_ListOfMainDomains_Edit, L("EditListOfMainDomain"), multiTenancySides: MultiTenancySides.Tenant);
            listOfMainDomains.CreateChildPermission(AppPermissions.Pages_ListOfMainDomains_Delete, L("DeleteListOfMainDomain"), multiTenancySides: MultiTenancySides.Tenant);

            var mainDomains = pages.CreateChildPermission(AppPermissions.Pages_MainDomains, L("MainDomains"), multiTenancySides: MultiTenancySides.Tenant);
            mainDomains.CreateChildPermission(AppPermissions.Pages_MainDomains_Create, L("CreateNewMainDomain"), multiTenancySides: MultiTenancySides.Tenant);
            mainDomains.CreateChildPermission(AppPermissions.Pages_MainDomains_Edit, L("EditMainDomain"), multiTenancySides: MultiTenancySides.Tenant);
            mainDomains.CreateChildPermission(AppPermissions.Pages_MainDomains_Delete, L("DeleteMainDomain"), multiTenancySides: MultiTenancySides.Tenant);

            var listOfClassScheduleModeSpaces = pages.CreateChildPermission(AppPermissions.Pages_ListOfClassScheduleModeSpaces, L("ListOfClassScheduleModeSpaces"), multiTenancySides: MultiTenancySides.Tenant);
            listOfClassScheduleModeSpaces.CreateChildPermission(AppPermissions.Pages_ListOfClassScheduleModeSpaces_Create, L("CreateNewListOfClassScheduleModeSpace"), multiTenancySides: MultiTenancySides.Tenant);
            listOfClassScheduleModeSpaces.CreateChildPermission(AppPermissions.Pages_ListOfClassScheduleModeSpaces_Edit, L("EditListOfClassScheduleModeSpace"), multiTenancySides: MultiTenancySides.Tenant);
            listOfClassScheduleModeSpaces.CreateChildPermission(AppPermissions.Pages_ListOfClassScheduleModeSpaces_Delete, L("DeleteListOfClassScheduleModeSpace"), multiTenancySides: MultiTenancySides.Tenant);

            var listOfAllCalculatedResults = pages.CreateChildPermission(AppPermissions.Pages_ListOfAllCalculatedResults, L("ListOfAllCalculatedResults"), multiTenancySides: MultiTenancySides.Tenant);
            listOfAllCalculatedResults.CreateChildPermission(AppPermissions.Pages_ListOfAllCalculatedResults_Create, L("CreateNewListOfAllCalculatedResult"), multiTenancySides: MultiTenancySides.Tenant);
            listOfAllCalculatedResults.CreateChildPermission(AppPermissions.Pages_ListOfAllCalculatedResults_Edit, L("EditListOfAllCalculatedResult"), multiTenancySides: MultiTenancySides.Tenant);
            listOfAllCalculatedResults.CreateChildPermission(AppPermissions.Pages_ListOfAllCalculatedResults_Delete, L("DeleteListOfAllCalculatedResult"), multiTenancySides: MultiTenancySides.Tenant);

            var classScheduleModeSpaces = pages.CreateChildPermission(AppPermissions.Pages_ClassScheduleModeSpaces, L("ClassScheduleModeSpaces"), multiTenancySides: MultiTenancySides.Tenant);
            classScheduleModeSpaces.CreateChildPermission(AppPermissions.Pages_ClassScheduleModeSpaces_Create, L("CreateNewClassScheduleModeSpace"), multiTenancySides: MultiTenancySides.Tenant);
            classScheduleModeSpaces.CreateChildPermission(AppPermissions.Pages_ClassScheduleModeSpaces_Edit, L("EditClassScheduleModeSpace"), multiTenancySides: MultiTenancySides.Tenant);
            classScheduleModeSpaces.CreateChildPermission(AppPermissions.Pages_ClassScheduleModeSpaces_Delete, L("DeleteClassScheduleModeSpace"), multiTenancySides: MultiTenancySides.Tenant);

            var classScheduleResults = pages.CreateChildPermission(AppPermissions.Pages_ClassScheduleResults, L("ClassScheduleResults"), multiTenancySides: MultiTenancySides.Tenant);
            classScheduleResults.CreateChildPermission(AppPermissions.Pages_ClassScheduleResults_Create, L("CreateNewClassScheduleResult"), multiTenancySides: MultiTenancySides.Tenant);
            classScheduleResults.CreateChildPermission(AppPermissions.Pages_ClassScheduleResults_Edit, L("EditClassScheduleResult"), multiTenancySides: MultiTenancySides.Tenant);
            classScheduleResults.CreateChildPermission(AppPermissions.Pages_ClassScheduleResults_Delete, L("DeleteClassScheduleResult"), multiTenancySides: MultiTenancySides.Tenant);

            var lessonsOfUniversityProfessors = pages.CreateChildPermission(AppPermissions.Pages_LessonsOfUniversityProfessors, L("LessonsOfUniversityProfessors"), multiTenancySides: MultiTenancySides.Tenant);
            lessonsOfUniversityProfessors.CreateChildPermission(AppPermissions.Pages_LessonsOfUniversityProfessors_Create, L("CreateNewLessonsOfUniversityProfessor"), multiTenancySides: MultiTenancySides.Tenant);
            lessonsOfUniversityProfessors.CreateChildPermission(AppPermissions.Pages_LessonsOfUniversityProfessors_Edit, L("EditLessonsOfUniversityProfessor"), multiTenancySides: MultiTenancySides.Tenant);
            lessonsOfUniversityProfessors.CreateChildPermission(AppPermissions.Pages_LessonsOfUniversityProfessors_Delete, L("DeleteLessonsOfUniversityProfessor"), multiTenancySides: MultiTenancySides.Tenant);

            var lessonsOfSemesters = pages.CreateChildPermission(AppPermissions.Pages_LessonsOfSemesters, L("LessonsOfSemesters"), multiTenancySides: MultiTenancySides.Tenant);
            lessonsOfSemesters.CreateChildPermission(AppPermissions.Pages_LessonsOfSemesters_Create, L("CreateNewLessonsOfSemester"), multiTenancySides: MultiTenancySides.Tenant);
            lessonsOfSemesters.CreateChildPermission(AppPermissions.Pages_LessonsOfSemesters_Edit, L("EditLessonsOfSemester"), multiTenancySides: MultiTenancySides.Tenant);
            lessonsOfSemesters.CreateChildPermission(AppPermissions.Pages_LessonsOfSemesters_Delete, L("DeleteLessonsOfSemester"), multiTenancySides: MultiTenancySides.Tenant);

            var lessons = pages.CreateChildPermission(AppPermissions.Pages_Lessons, L("Lessons"), multiTenancySides: MultiTenancySides.Tenant);
            lessons.CreateChildPermission(AppPermissions.Pages_Lessons_Create, L("CreateNewLesson"), multiTenancySides: MultiTenancySides.Tenant);
            lessons.CreateChildPermission(AppPermissions.Pages_Lessons_Edit, L("EditLesson"), multiTenancySides: MultiTenancySides.Tenant);
            lessons.CreateChildPermission(AppPermissions.Pages_Lessons_Delete, L("DeleteLesson"), multiTenancySides: MultiTenancySides.Tenant);

            var universityProfessorWorkingTimes = pages.CreateChildPermission(AppPermissions.Pages_UniversityProfessorWorkingTimes, L("UniversityProfessorWorkingTimes"), multiTenancySides: MultiTenancySides.Tenant);
            universityProfessorWorkingTimes.CreateChildPermission(AppPermissions.Pages_UniversityProfessorWorkingTimes_Create, L("CreateNewUniversityProfessorWorkingTime"), multiTenancySides: MultiTenancySides.Tenant);
            universityProfessorWorkingTimes.CreateChildPermission(AppPermissions.Pages_UniversityProfessorWorkingTimes_Edit, L("EditUniversityProfessorWorkingTime"), multiTenancySides: MultiTenancySides.Tenant);
            universityProfessorWorkingTimes.CreateChildPermission(AppPermissions.Pages_UniversityProfessorWorkingTimes_Delete, L("DeleteUniversityProfessorWorkingTime"), multiTenancySides: MultiTenancySides.Tenant);

            var universityProfessors = pages.CreateChildPermission(AppPermissions.Pages_UniversityProfessors, L("UniversityProfessors"), multiTenancySides: MultiTenancySides.Tenant);
            universityProfessors.CreateChildPermission(AppPermissions.Pages_UniversityProfessors_Create, L("CreateNewUniversityProfessor"), multiTenancySides: MultiTenancySides.Tenant);
            universityProfessors.CreateChildPermission(AppPermissions.Pages_UniversityProfessors_Edit, L("EditUniversityProfessor"), multiTenancySides: MultiTenancySides.Tenant);
            universityProfessors.CreateChildPermission(AppPermissions.Pages_UniversityProfessors_Delete, L("DeleteUniversityProfessor"), multiTenancySides: MultiTenancySides.Tenant);

            var workTimeInDays = pages.CreateChildPermission(AppPermissions.Pages_WorkTimeInDays, L("WorkTimeInDays"), multiTenancySides: MultiTenancySides.Tenant);
            workTimeInDays.CreateChildPermission(AppPermissions.Pages_WorkTimeInDays_Create, L("CreateNewWorkTimeInDay"), multiTenancySides: MultiTenancySides.Tenant);
            workTimeInDays.CreateChildPermission(AppPermissions.Pages_WorkTimeInDays_Edit, L("EditWorkTimeInDay"), multiTenancySides: MultiTenancySides.Tenant);
            workTimeInDays.CreateChildPermission(AppPermissions.Pages_WorkTimeInDays_Delete, L("DeleteWorkTimeInDay"), multiTenancySides: MultiTenancySides.Tenant);

            var assigningGradeToUniversityMajors = pages.CreateChildPermission(AppPermissions.Pages_AssigningGradeToUniversityMajors, L("AssigningGradeToUniversityMajors"), multiTenancySides: MultiTenancySides.Tenant);
            assigningGradeToUniversityMajors.CreateChildPermission(AppPermissions.Pages_AssigningGradeToUniversityMajors_Create, L("CreateNewAssigningGradeToUniversityMajor"), multiTenancySides: MultiTenancySides.Tenant);
            assigningGradeToUniversityMajors.CreateChildPermission(AppPermissions.Pages_AssigningGradeToUniversityMajors_Edit, L("EditAssigningGradeToUniversityMajor"), multiTenancySides: MultiTenancySides.Tenant);
            assigningGradeToUniversityMajors.CreateChildPermission(AppPermissions.Pages_AssigningGradeToUniversityMajors_Delete, L("DeleteAssigningGradeToUniversityMajor"), multiTenancySides: MultiTenancySides.Tenant);

            var semesters = pages.CreateChildPermission(AppPermissions.Pages_Semesters, L("Semesters"), multiTenancySides: MultiTenancySides.Tenant);
            semesters.CreateChildPermission(AppPermissions.Pages_Semesters_Create, L("CreateNewSemester"), multiTenancySides: MultiTenancySides.Tenant);
            semesters.CreateChildPermission(AppPermissions.Pages_Semesters_Edit, L("EditSemester"), multiTenancySides: MultiTenancySides.Tenant);
            semesters.CreateChildPermission(AppPermissions.Pages_Semesters_Delete, L("DeleteSemester"), multiTenancySides: MultiTenancySides.Tenant);

            var grades = pages.CreateChildPermission(AppPermissions.Pages_Grades, L("Grades"), multiTenancySides: MultiTenancySides.Tenant);
            grades.CreateChildPermission(AppPermissions.Pages_Grades_Create, L("CreateNewGrade"), multiTenancySides: MultiTenancySides.Tenant);
            grades.CreateChildPermission(AppPermissions.Pages_Grades_Edit, L("EditGrade"), multiTenancySides: MultiTenancySides.Tenant);
            grades.CreateChildPermission(AppPermissions.Pages_Grades_Delete, L("DeleteGrade"), multiTenancySides: MultiTenancySides.Tenant);

            var assigningUniversityMajorToClassroomBuildings = pages.CreateChildPermission(AppPermissions.Pages_AssigningUniversityMajorToClassroomBuildings, L("AssigningUniversityMajorToClassroomBuildings"), multiTenancySides: MultiTenancySides.Tenant);
            assigningUniversityMajorToClassroomBuildings.CreateChildPermission(AppPermissions.Pages_AssigningUniversityMajorToClassroomBuildings_Create, L("CreateNewAssigningUniversityMajorToClassroomBuilding"), multiTenancySides: MultiTenancySides.Tenant);
            assigningUniversityMajorToClassroomBuildings.CreateChildPermission(AppPermissions.Pages_AssigningUniversityMajorToClassroomBuildings_Edit, L("EditAssigningUniversityMajorToClassroomBuilding"), multiTenancySides: MultiTenancySides.Tenant);
            assigningUniversityMajorToClassroomBuildings.CreateChildPermission(AppPermissions.Pages_AssigningUniversityMajorToClassroomBuildings_Delete, L("DeleteAssigningUniversityMajorToClassroomBuilding"), multiTenancySides: MultiTenancySides.Tenant);

            var universityDepartments = pages.CreateChildPermission(AppPermissions.Pages_UniversityDepartments, L("UniversityDepartments"), multiTenancySides: MultiTenancySides.Tenant);
            universityDepartments.CreateChildPermission(AppPermissions.Pages_UniversityDepartments_Create, L("CreateNewUniversityDepartment"), multiTenancySides: MultiTenancySides.Tenant);
            universityDepartments.CreateChildPermission(AppPermissions.Pages_UniversityDepartments_Edit, L("EditUniversityDepartment"), multiTenancySides: MultiTenancySides.Tenant);
            universityDepartments.CreateChildPermission(AppPermissions.Pages_UniversityDepartments_Delete, L("DeleteUniversityDepartment"), multiTenancySides: MultiTenancySides.Tenant);

            var universityMajors = pages.CreateChildPermission(AppPermissions.Pages_UniversityMajors, L("UniversityMajors"), multiTenancySides: MultiTenancySides.Tenant);
            universityMajors.CreateChildPermission(AppPermissions.Pages_UniversityMajors_Create, L("CreateNewUniversityMajor"), multiTenancySides: MultiTenancySides.Tenant);
            universityMajors.CreateChildPermission(AppPermissions.Pages_UniversityMajors_Edit, L("EditUniversityMajor"), multiTenancySides: MultiTenancySides.Tenant);
            universityMajors.CreateChildPermission(AppPermissions.Pages_UniversityMajors_Delete, L("DeleteUniversityMajor"), multiTenancySides: MultiTenancySides.Tenant);

            var classroomBuildings = pages.CreateChildPermission(AppPermissions.Pages_ClassroomBuildings, L("ClassroomBuildings"), multiTenancySides: MultiTenancySides.Tenant);
            classroomBuildings.CreateChildPermission(AppPermissions.Pages_ClassroomBuildings_Create, L("CreateNewClassroomBuilding"), multiTenancySides: MultiTenancySides.Tenant);
            classroomBuildings.CreateChildPermission(AppPermissions.Pages_ClassroomBuildings_Edit, L("EditClassroomBuilding"), multiTenancySides: MultiTenancySides.Tenant);
            classroomBuildings.CreateChildPermission(AppPermissions.Pages_ClassroomBuildings_Delete, L("DeleteClassroomBuilding"), multiTenancySides: MultiTenancySides.Tenant);

            pages.CreateChildPermission(AppPermissions.Pages_DemoUiComponents, L("DemoUiComponents"));

            var administration = pages.CreateChildPermission(AppPermissions.Pages_Administration, L("Administration"));

            var roles = administration.CreateChildPermission(AppPermissions.Pages_Administration_Roles, L("Roles"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Create, L("CreatingNewRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Edit, L("EditingRole"));
            roles.CreateChildPermission(AppPermissions.Pages_Administration_Roles_Delete, L("DeletingRole"));

            var users = administration.CreateChildPermission(AppPermissions.Pages_Administration_Users, L("Users"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Create, L("CreatingNewUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Edit, L("EditingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Delete, L("DeletingUser"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_ChangePermissions, L("ChangingPermissions"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Impersonation, L("LoginForUsers"));
            users.CreateChildPermission(AppPermissions.Pages_Administration_Users_Unlock, L("Unlock"));

            var languages = administration.CreateChildPermission(AppPermissions.Pages_Administration_Languages, L("Languages"));
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Create, L("CreatingNewLanguage"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Edit, L("EditingLanguage"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_Delete, L("DeletingLanguages"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            languages.CreateChildPermission(AppPermissions.Pages_Administration_Languages_ChangeTexts, L("ChangingTexts"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_AuditLogs, L("AuditLogs"));

            var organizationUnits = administration.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits, L("OrganizationUnits"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageOrganizationTree, L("ManagingOrganizationTree"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageMembers, L("ManagingMembers"));
            organizationUnits.CreateChildPermission(AppPermissions.Pages_Administration_OrganizationUnits_ManageRoles, L("ManagingRoles"));

            administration.CreateChildPermission(AppPermissions.Pages_Administration_UiCustomization, L("VisualSettings"));

            var webhooks = administration.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription, L("Webhooks"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription_Create, L("CreatingWebhooks"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription_Edit, L("EditingWebhooks"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription_ChangeActivity, L("ChangingWebhookActivity"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_WebhookSubscription_Detail, L("DetailingSubscription"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_Webhook_ListSendAttempts, L("ListingSendAttempts"));
            webhooks.CreateChildPermission(AppPermissions.Pages_Administration_Webhook_ResendWebhook, L("ResendingWebhook"));

            var dynamicProperties = administration.CreateChildPermission(AppPermissions.Pages_Administration_DynamicProperties, L("DynamicProperties"));
            dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicProperties_Create, L("CreatingDynamicProperties"));
            dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicProperties_Edit, L("EditingDynamicProperties"));
            dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicProperties_Delete, L("DeletingDynamicProperties"));

            var dynamicPropertyValues = dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicPropertyValue, L("DynamicPropertyValue"));
            dynamicPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicPropertyValue_Create, L("CreatingDynamicPropertyValue"));
            dynamicPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicPropertyValue_Edit, L("EditingDynamicPropertyValue"));
            dynamicPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicPropertyValue_Delete, L("DeletingDynamicPropertyValue"));

            var dynamicEntityProperties = dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityProperties, L("DynamicEntityProperties"));
            dynamicEntityProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityProperties_Create, L("CreatingDynamicEntityProperties"));
            dynamicEntityProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityProperties_Edit, L("EditingDynamicEntityProperties"));
            dynamicEntityProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityProperties_Delete, L("DeletingDynamicEntityProperties"));

            var dynamicEntityPropertyValues = dynamicProperties.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityPropertyValue, L("EntityDynamicPropertyValue"));
            dynamicEntityPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityPropertyValue_Create, L("CreatingDynamicEntityPropertyValue"));
            dynamicEntityPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityPropertyValue_Edit, L("EditingDynamicEntityPropertyValue"));
            dynamicEntityPropertyValues.CreateChildPermission(AppPermissions.Pages_Administration_DynamicEntityPropertyValue_Delete, L("DeletingDynamicEntityPropertyValue"));

            //TENANT-SPECIFIC PERMISSIONS

            pages.CreateChildPermission(AppPermissions.Pages_Tenant_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Tenant);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Tenant_SubscriptionManagement, L("Subscription"), multiTenancySides: MultiTenancySides.Tenant);

            //HOST-SPECIFIC PERMISSIONS

            var editions = pages.CreateChildPermission(AppPermissions.Pages_Editions, L("Editions"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Create, L("CreatingNewEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Edit, L("EditingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_Delete, L("DeletingEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissions.Pages_Editions_MoveTenantsToAnotherEdition, L("MoveTenantsToAnotherEdition"), multiTenancySides: MultiTenancySides.Host);

            var tenants = pages.CreateChildPermission(AppPermissions.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Create, L("CreatingNewTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Edit, L("EditingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_ChangeFeatures, L("ChangingFeatures"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Delete, L("DeletingTenant"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissions.Pages_Tenants_Impersonation, L("LoginForTenants"), multiTenancySides: MultiTenancySides.Host);

            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Settings, L("Settings"), multiTenancySides: MultiTenancySides.Host);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Maintenance, L("Maintenance"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_HangfireDashboard, L("HangfireDashboard"), multiTenancySides: _isMultiTenancyEnabled ? MultiTenancySides.Host : MultiTenancySides.Tenant);
            administration.CreateChildPermission(AppPermissions.Pages_Administration_Host_Dashboard, L("Dashboard"), multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, ClassScheduleProjectConsts.LocalizationSourceName);
        }
    }
}