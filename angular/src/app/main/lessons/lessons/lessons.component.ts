import { AppConsts } from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
    LessonsServiceProxy,
    LessonDto,
    LessonTypeEnum,
    ClassificationLessonEnum,
} from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditLessonModalComponent } from './create-or-edit-lesson-modal.component';

import { ViewLessonModalComponent } from './view-lesson-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/table';
import { Paginator } from 'primeng/paginator';
import { LazyLoadEvent } from 'primeng/api';
import { FileDownloadService } from '@shared/utils/file-download.service';
import { EntityTypeHistoryModalComponent } from '@app/shared/common/entityHistory/entity-type-history-modal.component';
import { filter as _filter } from 'lodash-es';
import { DateTime } from 'luxon';

import { DateTimeService } from '@app/shared/common/timing/date-time.service';

@Component({
    templateUrl: './lessons.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class LessonsComponent extends AppComponentBase {
    @ViewChild('entityTypeHistoryModal', { static: true }) entityTypeHistoryModal: EntityTypeHistoryModalComponent;
    @ViewChild('createOrEditLessonModal', { static: true }) createOrEditLessonModal: CreateOrEditLessonModalComponent;
    @ViewChild('viewLessonModalComponent', { static: true }) viewLessonModal: ViewLessonModalComponent;

    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    nameLessonFilter = '';
    maxHoursPerWeekFilter: number;
    maxHoursPerWeekFilterEmpty: number;
    minHoursPerWeekFilter: number;
    minHoursPerWeekFilterEmpty: number;
    lessonTypeFilter = -1;
    classificationLessonFilter = -1;
    maxNumberOfUnitsFilter: number;
    maxNumberOfUnitsFilterEmpty: number;
    minNumberOfUnitsFilter: number;
    minNumberOfUnitsFilterEmpty: number;
    maxNumberOfGroupsFilter: number;
    maxNumberOfGroupsFilterEmpty: number;
    minNumberOfGroupsFilter: number;
    minNumberOfGroupsFilterEmpty: number;
    isActiveFilter = -1;
    classroomBuildingClassroomBuildingNameFilter = '';

    lessonTypeEnum = LessonTypeEnum;
    classificationLessonEnum = ClassificationLessonEnum;

    _entityTypeFullName = 'Mohajer.ClassScheduleProject.CentralUnit.Lessons.Lesson';
    entityHistoryEnabled = false;

    constructor(
        injector: Injector,
        private _lessonsServiceProxy: LessonsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
        private _dateTimeService: DateTimeService
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.entityHistoryEnabled = this.setIsEntityHistoryEnabled();
    }

    private setIsEntityHistoryEnabled(): boolean {
        let customSettings = (abp as any).custom;
        return (
            this.isGrantedAny('Pages.Administration.AuditLogs') &&
            customSettings.EntityHistory &&
            customSettings.EntityHistory.isEnabled &&
            _filter(
                customSettings.EntityHistory.enabledEntities,
                (entityType) => entityType === this._entityTypeFullName
            ).length === 1
        );
    }

    getLessons(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._lessonsServiceProxy
            .getAll(
                this.filterText,
                this.nameLessonFilter,
                this.maxHoursPerWeekFilter == null ? this.maxHoursPerWeekFilterEmpty : this.maxHoursPerWeekFilter,
                this.minHoursPerWeekFilter == null ? this.minHoursPerWeekFilterEmpty : this.minHoursPerWeekFilter,
                this.lessonTypeFilter,
                this.classificationLessonFilter,
                this.maxNumberOfUnitsFilter == null ? this.maxNumberOfUnitsFilterEmpty : this.maxNumberOfUnitsFilter,
                this.minNumberOfUnitsFilter == null ? this.minNumberOfUnitsFilterEmpty : this.minNumberOfUnitsFilter,
                this.maxNumberOfGroupsFilter == null ? this.maxNumberOfGroupsFilterEmpty : this.maxNumberOfGroupsFilter,
                this.minNumberOfGroupsFilter == null ? this.minNumberOfGroupsFilterEmpty : this.minNumberOfGroupsFilter,
                this.isActiveFilter,
                this.classroomBuildingClassroomBuildingNameFilter,
                this.primengTableHelper.getSorting(this.dataTable),
                this.primengTableHelper.getSkipCount(this.paginator, event),
                this.primengTableHelper.getMaxResultCount(this.paginator, event)
            )
            .subscribe((result) => {
                this.primengTableHelper.totalRecordsCount = result.totalCount;
                this.primengTableHelper.records = result.items;
                this.primengTableHelper.hideLoadingIndicator();
            });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    createLesson(): void {
        this.createOrEditLessonModal.show();
    }

    showHistory(lesson: LessonDto): void {
        this.entityTypeHistoryModal.show({
            entityId: lesson.id.toString(),
            entityTypeFullName: this._entityTypeFullName,
            entityTypeDescription: '',
        });
    }

    deleteLesson(lesson: LessonDto): void {
        this.message.confirm('', this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._lessonsServiceProxy.delete(lesson.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    exportToExcel(): void {
        this._lessonsServiceProxy
            .getLessonsToExcel(
                this.filterText,
                this.nameLessonFilter,
                this.maxHoursPerWeekFilter == null ? this.maxHoursPerWeekFilterEmpty : this.maxHoursPerWeekFilter,
                this.minHoursPerWeekFilter == null ? this.minHoursPerWeekFilterEmpty : this.minHoursPerWeekFilter,
                this.lessonTypeFilter,
                this.classificationLessonFilter,
                this.maxNumberOfUnitsFilter == null ? this.maxNumberOfUnitsFilterEmpty : this.maxNumberOfUnitsFilter,
                this.minNumberOfUnitsFilter == null ? this.minNumberOfUnitsFilterEmpty : this.minNumberOfUnitsFilter,
                this.maxNumberOfGroupsFilter == null ? this.maxNumberOfGroupsFilterEmpty : this.maxNumberOfGroupsFilter,
                this.minNumberOfGroupsFilter == null ? this.minNumberOfGroupsFilterEmpty : this.minNumberOfGroupsFilter,
                this.isActiveFilter,
                this.classroomBuildingClassroomBuildingNameFilter
            )
            .subscribe((result) => {
                this._fileDownloadService.downloadTempFile(result);
            });
    }
}
