﻿import { PermissionCheckerService } from 'abp-ng2-module';
import { AppSessionService } from '@shared/common/session/app-session.service';

import { Injectable } from '@angular/core';
import { AppMenu } from './app-menu';
import { AppMenuItem } from './app-menu-item';

@Injectable()
export class AppNavigationService {

    constructor(
        private _permissionCheckerService: PermissionCheckerService,
        private _appSessionService: AppSessionService
    ) {

    }

    getMenu(): AppMenu {
        return new AppMenu('MainMenu', 'MainMenu', [
            new AppMenuItem('Dashboard', 'Pages.Administration.Host.Dashboard', 'flaticon-line-graph', '/app/admin/hostDashboard'),
            new AppMenuItem('Dashboard', 'Pages.Tenant.Dashboard', 'flaticon-line-graph', '/app/main/dashboard'),
            new AppMenuItem('Tenants', 'Pages.Tenants', 'flaticon-list-3', '/app/admin/tenants'),
            new AppMenuItem('Editions', 'Pages.Editions', 'flaticon-app', '/app/admin/editions'),

            new AppMenuItem('ClassroomBuildings', 'Pages.ClassroomBuildings', 'flaticon-more', '/app/main/classroomBuildings/classroomBuildings'),

            new AppMenuItem('UniversityMajors', 'Pages.UniversityMajors', 'flaticon-more', '/app/main/universityMajors/universityMajors'),

            new AppMenuItem('UniversityDepartments', 'Pages.UniversityDepartments', 'flaticon-more', '/app/main/universityDepartments/universityDepartments'),


            new AppMenuItem('AssigningUniversityMajorToClassroomBuildings', 'Pages.AssigningUniversityMajorToClassroomBuildings', 'flaticon-more', '/app/main/assigningUniversityMajorToClassroomBuildings/assigningUniversityMajorToClassroomBuildings'),

            new AppMenuItem('Grades', 'Pages.Grades', 'flaticon-more', '/app/main/grades/grades'),

            new AppMenuItem('Semesters', 'Pages.Semesters', 'flaticon-more', '/app/main/semesters/semesters'),

            new AppMenuItem('AssigningGradeToUniversityMajors', 'Pages.AssigningGradeToUniversityMajors', 'flaticon-more', '/app/main/assigningGradeToUniversityMajors/assigningGradeToUniversityMajors'),

            new AppMenuItem('WorkTimeInDays', 'Pages.WorkTimeInDays', 'flaticon-more', '/app/main/workTimeInDays/workTimeInDays'),

            new AppMenuItem('UniversityProfessors', 'Pages.UniversityProfessors', 'flaticon-more', '/app/main/universityProfessors/universityProfessors'),

            new AppMenuItem('UniversityProfessorWorkingTimes', 'Pages.UniversityProfessorWorkingTimes', 'flaticon-more', '/app/main/universityProfessorWorkingTimes/universityProfessorWorkingTimes'),

            new AppMenuItem('Lessons', 'Pages.Lessons', 'flaticon-more', '/app/main/lessons/lessons'),

            new AppMenuItem('LessonsOfSemesters', 'Pages.LessonsOfSemesters', 'flaticon-more', '/app/main/lessonsOfSemesters/lessonsOfSemesters'),

            new AppMenuItem('LessonsOfUniversityProfessors', 'Pages.LessonsOfUniversityProfessors', 'flaticon-more', '/app/main/lessonsOfUniversityProfessors/lessonsOfUniversityProfessors'),

          //  new AppMenuItem('ClassScheduleResults', 'Pages.ClassScheduleResults', 'flaticon-more', '/app/main/classScheduleResults/classScheduleResults'),

          //  new AppMenuItem('ClassScheduleModeSpaces', 'Pages.ClassScheduleModeSpaces', 'flaticon-more', '/app/main/classScheduleModeSpaces/classScheduleModeSpaces'),

            new AppMenuItem('ListOfAllCalculatedResults', 'Pages.ListOfAllCalculatedResults', 'flaticon-more', '/app/main/listOfAllCalculatedResults/listOfAllCalculatedResults'),

        //    new AppMenuItem('MainDomains', 'Pages.MainDomains', 'flaticon-more', '/app/main/mainDomains/mainDomains'),

         //   new AppMenuItem('ListOfMainDomains', 'Pages.ListOfMainDomains', 'flaticon-more', '/app/main/listOfMainDomains/listOfMainDomains'),
             new AppMenuItem('Administration', '', 'flaticon-interface-8', '', [], [
                new AppMenuItem('OrganizationUnits', 'Pages.Administration.OrganizationUnits', 'flaticon-map', '/app/admin/organization-units'),
                new AppMenuItem('Roles', 'Pages.Administration.Roles', 'flaticon-suitcase', '/app/admin/roles'),
                new AppMenuItem('Users', 'Pages.Administration.Users', 'flaticon-users', '/app/admin/users'),
                new AppMenuItem('Languages', 'Pages.Administration.Languages', 'flaticon-tabs', '/app/admin/languages', ['/app/admin/languages/{name}/texts']),
                new AppMenuItem('AuditLogs', 'Pages.Administration.AuditLogs', 'flaticon-folder-1', '/app/admin/auditLogs'),
                new AppMenuItem('Maintenance', 'Pages.Administration.Host.Maintenance', 'flaticon-lock', '/app/admin/maintenance'),
                new AppMenuItem('Subscription', 'Pages.Administration.Tenant.SubscriptionManagement', 'flaticon-refresh', '/app/admin/subscription-management'),

                new AppMenuItem('WebhookSubscriptions', 'Pages.Administration.WebhookSubscription', 'flaticon2-world', '/app/admin/webhook-subscriptions'),
                new AppMenuItem('DynamicProperties', 'Pages.Administration.DynamicProperties', 'flaticon-interface-8', '/app/admin/dynamic-property'),
                new AppMenuItem('Settings', 'Pages.Administration.Host.Settings', 'flaticon-settings', '/app/admin/hostSettings'),
                new AppMenuItem('Settings', 'Pages.Administration.Tenant.Settings', 'flaticon-settings', '/app/admin/tenantSettings')
            ]),
        ]);
    }

    checkChildMenuItemPermission(menuItem): boolean {

        for (let i = 0; i < menuItem.items.length; i++) {
            let subMenuItem = menuItem.items[i];
            if (subMenuItem.permissionName === '' || subMenuItem.permissionName === null) {
                if (subMenuItem.route) {
                    return true;
                }
            } else if (this._permissionCheckerService.isGranted(subMenuItem.permissionName)) {
                return true;
            }
            if (subMenuItem.items && subMenuItem.items.length) {
                let isAnyChildItemActive = this.checkChildMenuItemPermission(subMenuItem);
                if (isAnyChildItemActive) {
                    return true;
                }
            }
        }
        return false;
    }

    showMenuItem(menuItem: AppMenuItem): boolean {
        if (menuItem.permissionName === 'Pages.Administration.Tenant.SubscriptionManagement' && this._appSessionService.tenant && !this._appSessionService.tenant.edition) {
            return false;
        }
        let hideMenuItem = false;
        if (menuItem.requiresAuthentication && !this._appSessionService.user) {
            hideMenuItem = true;
        }
        if (menuItem.permissionName && !this._permissionCheckerService.isGranted(menuItem.permissionName)) {
            hideMenuItem = true;
        }
        if (this._appSessionService.tenant || !abp.multiTenancy.ignoreFeatureCheckForHostUsers) {
            if (menuItem.hasFeatureDependency() && !menuItem.featureDependencySatisfied()) {
                hideMenuItem = true;
            }
        }
        if (!hideMenuItem && menuItem.items && menuItem.items.length) {
            return this.checkChildMenuItemPermission(menuItem);
        }
        return !hideMenuItem;
    }

    getAllMenuItems(): AppMenuItem[] {
        let menu = this.getMenu();
        let allMenuItems: AppMenuItem[] = [];
        menu.items.forEach(menuItem => {
            allMenuItems = allMenuItems.concat(this.getAllMenuItemsRecursive(menuItem));
        });
        return allMenuItems;
    }

    private getAllMenuItemsRecursive(menuItem: AppMenuItem): AppMenuItem[] {
        if (!menuItem.items) {
            return [menuItem];
        }
        let menuItems = [menuItem];
        menuItem.items.forEach(subMenu => {
            menuItems = menuItems.concat(this.getAllMenuItemsRecursive(subMenu));
        });
        return menuItems;
    }
}
