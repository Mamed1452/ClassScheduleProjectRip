﻿using Mohajer.ClassScheduleProject.CentralUnit.ListOfMainDomains.Dtos;
using Mohajer.ClassScheduleProject.CentralUnit.ListOfMainDomains;
using Mohajer.ClassScheduleProject.CentralUnit.MainDomains.Dtos;
using Mohajer.ClassScheduleProject.CentralUnit.MainDomains;
using Mohajer.ClassScheduleProject.CentralUnit.ListOfClassScheduleModeSpaces.Dtos;
using Mohajer.ClassScheduleProject.CentralUnit.ListOfClassScheduleModeSpaces;
using Mohajer.ClassScheduleProject.CentralUnit.ListOfAllCalculatedResults.Dtos;
using Mohajer.ClassScheduleProject.CentralUnit.ListOfAllCalculatedResults;
using Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleModeSpaces.Dtos;
using Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleModeSpaces;
using Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleResults.Dtos;
using Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleResults;
using Mohajer.ClassScheduleProject.CentralUnit.LessonsOfUniversityProfessors.Dtos;
using Mohajer.ClassScheduleProject.CentralUnit.LessonsOfUniversityProfessors;
using Mohajer.ClassScheduleProject.CentralUnit.LessonsOfSemesters.Dtos;
using Mohajer.ClassScheduleProject.CentralUnit.LessonsOfSemesters;
using Mohajer.ClassScheduleProject.CentralUnit.Lessons.Dtos;
using Mohajer.ClassScheduleProject.CentralUnit.Lessons;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessorWorkingTimes.Dtos;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessorWorkingTimes;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessors.Dtos;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityProfessors;
using Mohajer.ClassScheduleProject.CentralUnit.WorkTimeInDays.Dtos;
using Mohajer.ClassScheduleProject.CentralUnit.WorkTimeInDays;
using Mohajer.ClassScheduleProject.CentralUnit.AssigningGradeToUniversityMajors.Dtos;
using Mohajer.ClassScheduleProject.CentralUnit.AssigningGradeToUniversityMajors;
using Mohajer.ClassScheduleProject.CentralUnit.Semesters.Dtos;
using Mohajer.ClassScheduleProject.CentralUnit.Semesters;
using Mohajer.ClassScheduleProject.CentralUnit.Grades.Dtos;
using Mohajer.ClassScheduleProject.CentralUnit.Grades;
using Mohajer.ClassScheduleProject.CentralUnit.AssigningUniversityMajorToClassroomBuildings.Dtos;
using Mohajer.ClassScheduleProject.CentralUnit.AssigningUniversityMajorToClassroomBuildings;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityDepartments.Dtos;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityDepartments;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityMajors.Dtos;
using Mohajer.ClassScheduleProject.CentralUnit.UniversityMajors;
using Mohajer.ClassScheduleProject.CentralUnit.ClassroomBuildings.Dtos;
using Mohajer.ClassScheduleProject.CentralUnit.ClassroomBuildings;
using System;
using System.Linq;
using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Domain.Entities;
using Abp.DynamicEntityProperties;
using Abp.EntityHistory;
using Abp.Localization;
using Abp.Notifications;
using Abp.Organizations;
using Abp.UI.Inputs;
using Abp.Webhooks;
using AutoMapper;
using Mohajer.ClassScheduleProject.Auditing.Dto;
using Mohajer.ClassScheduleProject.Authorization.Accounts.Dto;
using Mohajer.ClassScheduleProject.Authorization.Delegation;
using Mohajer.ClassScheduleProject.Authorization.Permissions.Dto;
using Mohajer.ClassScheduleProject.Authorization.Roles;
using Mohajer.ClassScheduleProject.Authorization.Roles.Dto;
using Mohajer.ClassScheduleProject.Authorization.Users;
using Mohajer.ClassScheduleProject.Authorization.Users.Delegation.Dto;
using Mohajer.ClassScheduleProject.Authorization.Users.Dto;
using Mohajer.ClassScheduleProject.Authorization.Users.Importing.Dto;
using Mohajer.ClassScheduleProject.Authorization.Users.Profile.Dto;
using Mohajer.ClassScheduleProject.Chat;
using Mohajer.ClassScheduleProject.Chat.Dto;
using Mohajer.ClassScheduleProject.Dto;
using Mohajer.ClassScheduleProject.DynamicEntityProperties.Dto;
using Mohajer.ClassScheduleProject.Editions;
using Mohajer.ClassScheduleProject.Editions.Dto;

using Mohajer.ClassScheduleProject.Friendships;
using Mohajer.ClassScheduleProject.Friendships.Cache;
using Mohajer.ClassScheduleProject.Friendships.Dto;
using Mohajer.ClassScheduleProject.Localization.Dto;
using Mohajer.ClassScheduleProject.MultiTenancy;
using Mohajer.ClassScheduleProject.MultiTenancy.Dto;
using Mohajer.ClassScheduleProject.MultiTenancy.HostDashboard.Dto;
using Mohajer.ClassScheduleProject.MultiTenancy.Payments;
using Mohajer.ClassScheduleProject.MultiTenancy.Payments.Dto;
using Mohajer.ClassScheduleProject.Notifications;
using Mohajer.ClassScheduleProject.Notifications.Dto;
using Mohajer.ClassScheduleProject.Organizations.Dto;
using Mohajer.ClassScheduleProject.Sessions.Dto;
using Mohajer.ClassScheduleProject.WebHooks.Dto;
using Abp.Extensions;

namespace Mohajer.ClassScheduleProject
{
    internal static class CustomDtoMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<CreateOrEditListOfMainDomainDto, ListOfMainDomain>().ReverseMap();
            configuration.CreateMap<ListOfMainDomainDto, ListOfMainDomain>().ReverseMap();
            configuration.CreateMap<CreateOrEditMainDomainDto, MainDomain>().ReverseMap();
            configuration.CreateMap<MainDomainDto, MainDomain>().ReverseMap();
            configuration.CreateMap<CreateOrEditListOfClassScheduleModeSpaceDto, ListOfClassScheduleModeSpace>().ReverseMap();
            configuration.CreateMap<ListOfClassScheduleModeSpaceDto, ListOfClassScheduleModeSpace>().ReverseMap();
            configuration.CreateMap<CreateOrEditListOfAllCalculatedResultDto, ListOfAllCalculatedResult>().ReverseMap();
            configuration.CreateMap<ListOfAllCalculatedResultDto, ListOfAllCalculatedResult>().ReverseMap();
            configuration.CreateMap<CreateOrEditClassScheduleModeSpaceDto, ClassScheduleModeSpace>().ReverseMap();
            configuration.CreateMap<ClassScheduleModeSpaceDto, ClassScheduleModeSpace>().ReverseMap();
            configuration.CreateMap<CreateOrEditClassScheduleResultDto, ClassScheduleResult>().ReverseMap();
            configuration.CreateMap<ClassScheduleResultDto, ClassScheduleResult>().ReverseMap();
            configuration.CreateMap<CreateOrEditLessonsOfUniversityProfessorDto, LessonsOfUniversityProfessor>().ReverseMap();
            configuration.CreateMap<LessonsOfUniversityProfessorDto, LessonsOfUniversityProfessor>().ReverseMap();
            configuration.CreateMap<CreateOrEditLessonsOfSemesterDto, LessonsOfSemester>().ReverseMap();
            configuration.CreateMap<LessonsOfSemesterDto, LessonsOfSemester>().ReverseMap();
            configuration.CreateMap<CreateOrEditLessonDto, Lesson>().ReverseMap();
            configuration.CreateMap<LessonDto, Lesson>().ReverseMap();
            configuration.CreateMap<CreateOrEditUniversityProfessorWorkingTimeDto, UniversityProfessorWorkingTime>().ReverseMap();
            configuration.CreateMap<UniversityProfessorWorkingTimeDto, UniversityProfessorWorkingTime>().ReverseMap();
            configuration.CreateMap<CreateOrEditUniversityProfessorDto, UniversityProfessor>().ReverseMap();
            configuration.CreateMap<UniversityProfessorDto, UniversityProfessor>().ReverseMap();
            configuration.CreateMap<CreateOrEditWorkTimeInDayDto, WorkTimeInDay>().ReverseMap();
            configuration.CreateMap<WorkTimeInDayDto, WorkTimeInDay>().ReverseMap();
            configuration.CreateMap<CreateOrEditAssigningGradeToUniversityMajorDto, AssigningGradeToUniversityMajor>().ReverseMap();
            configuration.CreateMap<AssigningGradeToUniversityMajorDto, AssigningGradeToUniversityMajor>().ReverseMap();
            configuration.CreateMap<CreateOrEditSemesterDto, Semester>().ReverseMap();
            configuration.CreateMap<SemesterDto, Semester>().ReverseMap();
            configuration.CreateMap<CreateOrEditGradeDto, Grade>().ReverseMap();
            configuration.CreateMap<GradeDto, Grade>().ReverseMap();
            configuration.CreateMap<CreateOrEditAssigningUniversityMajorToClassroomBuildingDto, AssigningUniversityMajorToClassroomBuilding>().ReverseMap();
            configuration.CreateMap<AssigningUniversityMajorToClassroomBuildingDto, AssigningUniversityMajorToClassroomBuilding>().ReverseMap();
            configuration.CreateMap<CreateOrEditUniversityDepartmentDto, UniversityDepartment>().ReverseMap();
            configuration.CreateMap<UniversityDepartmentDto, UniversityDepartment>().ReverseMap();
            configuration.CreateMap<CreateOrEditUniversityMajorDto, UniversityMajor>().ReverseMap();
            configuration.CreateMap<UniversityMajorDto, UniversityMajor>().ReverseMap();
            configuration.CreateMap<CreateOrEditClassroomBuildingDto, ClassroomBuilding>().ReverseMap();
            configuration.CreateMap<ClassroomBuildingDto, ClassroomBuilding>().ReverseMap();
            //Inputs
            configuration.CreateMap<CheckboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<SingleLineStringInputType, FeatureInputTypeDto>();
            configuration.CreateMap<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<IInputType, FeatureInputTypeDto>()
                .Include<CheckboxInputType, FeatureInputTypeDto>()
                .Include<SingleLineStringInputType, FeatureInputTypeDto>()
                .Include<ComboboxInputType, FeatureInputTypeDto>();
            configuration.CreateMap<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<ILocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>()
                .Include<StaticLocalizableComboboxItemSource, LocalizableComboboxItemSourceDto>();
            configuration.CreateMap<LocalizableComboboxItem, LocalizableComboboxItemDto>();
            configuration.CreateMap<ILocalizableComboboxItem, LocalizableComboboxItemDto>()
                .Include<LocalizableComboboxItem, LocalizableComboboxItemDto>();

            //Chat
            configuration.CreateMap<ChatMessage, ChatMessageDto>();
            configuration.CreateMap<ChatMessage, ChatMessageExportDto>();

            //Feature
            configuration.CreateMap<FlatFeatureSelectDto, Feature>().ReverseMap();
            configuration.CreateMap<Feature, FlatFeatureDto>();

            //Role
            configuration.CreateMap<RoleEditDto, Role>().ReverseMap();
            configuration.CreateMap<Role, RoleListDto>();
            configuration.CreateMap<UserRole, UserListRoleDto>();

            //Edition
            configuration.CreateMap<EditionEditDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<EditionCreateDto, SubscribableEdition>();
            configuration.CreateMap<EditionSelectDto, SubscribableEdition>().ReverseMap();
            configuration.CreateMap<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<Edition, EditionInfoDto>().Include<SubscribableEdition, EditionInfoDto>();

            configuration.CreateMap<SubscribableEdition, EditionListDto>();
            configuration.CreateMap<Edition, EditionEditDto>();
            configuration.CreateMap<Edition, SubscribableEdition>();
            configuration.CreateMap<Edition, EditionSelectDto>();

            //Payment
            configuration.CreateMap<SubscriptionPaymentDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPaymentListDto, SubscriptionPayment>().ReverseMap();
            configuration.CreateMap<SubscriptionPayment, SubscriptionPaymentInfoDto>();

            //Permission
            configuration.CreateMap<Permission, FlatPermissionDto>();
            configuration.CreateMap<Permission, FlatPermissionWithLevelDto>();

            //Language
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageListDto>();
            configuration.CreateMap<NotificationDefinition, NotificationSubscriptionWithDisplayNameDto>();
            configuration.CreateMap<ApplicationLanguage, ApplicationLanguageEditDto>()
                .ForMember(ldto => ldto.IsEnabled, options => options.MapFrom(l => !l.IsDisabled));

            //Tenant
            configuration.CreateMap<Tenant, RecentTenant>();
            configuration.CreateMap<Tenant, TenantLoginInfoDto>();
            configuration.CreateMap<Tenant, TenantListDto>();
            configuration.CreateMap<TenantEditDto, Tenant>().ReverseMap();
            configuration.CreateMap<CurrentTenantInfoDto, Tenant>().ReverseMap();

            //User
            configuration.CreateMap<User, UserEditDto>()
                .ForMember(dto => dto.Password, options => options.Ignore())
                .ReverseMap()
                .ForMember(user => user.Password, options => options.Ignore());
            configuration.CreateMap<User, UserLoginInfoDto>();
            configuration.CreateMap<User, UserListDto>();
            configuration.CreateMap<User, ChatUserDto>();
            configuration.CreateMap<User, OrganizationUnitUserListDto>();
            configuration.CreateMap<Role, OrganizationUnitRoleListDto>();
            configuration.CreateMap<CurrentUserProfileEditDto, User>().ReverseMap();
            configuration.CreateMap<UserLoginAttemptDto, UserLoginAttempt>().ReverseMap();
            configuration.CreateMap<ImportUserDto, User>();

            //AuditLog
            configuration.CreateMap<AuditLog, AuditLogListDto>();
            configuration.CreateMap<EntityChange, EntityChangeListDto>();
            configuration.CreateMap<EntityPropertyChange, EntityPropertyChangeDto>();

            //Friendship
            configuration.CreateMap<Friendship, FriendDto>();
            configuration.CreateMap<FriendCacheItem, FriendDto>();

            //OrganizationUnit
            configuration.CreateMap<OrganizationUnit, OrganizationUnitDto>();

            //Webhooks
            configuration.CreateMap<WebhookSubscription, GetAllSubscriptionsOutput>();
            configuration.CreateMap<WebhookSendAttempt, GetAllSendAttemptsOutput>()
                .ForMember(webhookSendAttemptListDto => webhookSendAttemptListDto.WebhookName,
                    options => options.MapFrom(l => l.WebhookEvent.WebhookName))
                .ForMember(webhookSendAttemptListDto => webhookSendAttemptListDto.Data,
                    options => options.MapFrom(l => l.WebhookEvent.Data));

            configuration.CreateMap<WebhookSendAttempt, GetAllSendAttemptsOfWebhookEventOutput>();

            configuration.CreateMap<DynamicProperty, DynamicPropertyDto>().ReverseMap();
            configuration.CreateMap<DynamicPropertyValue, DynamicPropertyValueDto>().ReverseMap();
            configuration.CreateMap<DynamicEntityProperty, DynamicEntityPropertyDto>()
                .ForMember(dto => dto.DynamicPropertyName,
                    options => options.MapFrom(entity => entity.DynamicProperty.DisplayName));
            configuration.CreateMap<DynamicEntityPropertyDto, DynamicEntityProperty>();

            configuration.CreateMap<DynamicEntityPropertyValue, DynamicEntityPropertyValueDto>().ReverseMap();

            //User Delegations
            configuration.CreateMap<CreateUserDelegationDto, UserDelegation>();

            /* ADD YOUR OWN CUSTOM AUTOMAPPER MAPPINGS HERE */

        }
    }
}