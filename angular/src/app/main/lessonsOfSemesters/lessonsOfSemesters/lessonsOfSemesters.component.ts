import { AppConsts } from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
    LessonsOfSemestersServiceProxy,
    LessonsOfSemesterDto,
    LessonOfSemesterTypeEnum,
} from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditLessonsOfSemesterModalComponent } from './create-or-edit-lessonsOfSemester-modal.component';

import { ViewLessonsOfSemesterModalComponent } from './view-lessonsOfSemester-modal.component';
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
    templateUrl: './lessonsOfSemesters.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class LessonsOfSemestersComponent extends AppComponentBase {
    @ViewChild('entityTypeHistoryModal', { static: true }) entityTypeHistoryModal: EntityTypeHistoryModalComponent;
    @ViewChild('createOrEditLessonsOfSemesterModal', { static: true })
    createOrEditLessonsOfSemesterModal: CreateOrEditLessonsOfSemesterModalComponent;
    @ViewChild('viewLessonsOfSemesterModalComponent', { static: true })
    viewLessonsOfSemesterModal: ViewLessonsOfSemesterModalComponent;

    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    lessonOfSemesterTypeFilter = -1;
    maxNumberOfClassesToBeFormedFilter: number;
    maxNumberOfClassesToBeFormedFilterEmpty: number;
    minNumberOfClassesToBeFormedFilter: number;
    minNumberOfClassesToBeFormedFilterEmpty: number;
    isActiveFilter = -1;
    lessonsOfSemesterNameFilter = '';
    lessonNameLessonFilter = '';
    semesterSemesterNameFilter = '';

    lessonOfSemesterTypeEnum = LessonOfSemesterTypeEnum;

    _entityTypeFullName = 'Mohajer.ClassScheduleProject.CentralUnit.LessonsOfSemesters.LessonsOfSemester';
    entityHistoryEnabled = false;

    constructor(
        injector: Injector,
        private _lessonsOfSemestersServiceProxy: LessonsOfSemestersServiceProxy,
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

    getLessonsOfSemesters(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._lessonsOfSemestersServiceProxy
            .getAll(
                this.filterText,
                this.lessonOfSemesterTypeFilter,
                this.maxNumberOfClassesToBeFormedFilter == null
                    ? this.maxNumberOfClassesToBeFormedFilterEmpty
                    : this.maxNumberOfClassesToBeFormedFilter,
                this.minNumberOfClassesToBeFormedFilter == null
                    ? this.minNumberOfClassesToBeFormedFilterEmpty
                    : this.minNumberOfClassesToBeFormedFilter,
                this.isActiveFilter,
                this.lessonsOfSemesterNameFilter,
                this.lessonNameLessonFilter,
                this.semesterSemesterNameFilter,
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

    createLessonsOfSemester(): void {
        this.createOrEditLessonsOfSemesterModal.show();
    }

    showHistory(lessonsOfSemester: LessonsOfSemesterDto): void {
        this.entityTypeHistoryModal.show({
            entityId: lessonsOfSemester.id.toString(),
            entityTypeFullName: this._entityTypeFullName,
            entityTypeDescription: '',
        });
    }

    deleteLessonsOfSemester(lessonsOfSemester: LessonsOfSemesterDto): void {
        this.message.confirm('', this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._lessonsOfSemestersServiceProxy.delete(lessonsOfSemester.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    exportToExcel(): void {
        this._lessonsOfSemestersServiceProxy
            .getLessonsOfSemestersToExcel(
                this.filterText,
                this.lessonOfSemesterTypeFilter,
                this.maxNumberOfClassesToBeFormedFilter == null
                    ? this.maxNumberOfClassesToBeFormedFilterEmpty
                    : this.maxNumberOfClassesToBeFormedFilter,
                this.minNumberOfClassesToBeFormedFilter == null
                    ? this.minNumberOfClassesToBeFormedFilterEmpty
                    : this.minNumberOfClassesToBeFormedFilter,
                this.isActiveFilter,
                this.lessonsOfSemesterNameFilter,
                this.lessonNameLessonFilter,
                this.semesterSemesterNameFilter
            )
            .subscribe((result) => {
                this._fileDownloadService.downloadTempFile(result);
            });
    }
}
