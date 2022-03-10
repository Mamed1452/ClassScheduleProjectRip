import { AppConsts } from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ClassScheduleResultsServiceProxy, ClassScheduleResultDto } from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { MasterDetailChild_ClassScheduleModeSpace_CreateOrEditClassScheduleResultModalComponent } from './masterDetailChild_ClassScheduleModeSpace_create-or-edit-classScheduleResult-modal.component';

import { MasterDetailChild_ClassScheduleModeSpace_ViewClassScheduleResultModalComponent } from './masterDetailChild_ClassScheduleModeSpace_view-classScheduleResult-modal.component';
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
    templateUrl: './masterDetailChild_ClassScheduleModeSpace_classScheduleResults.component.html',
    selector: 'masterDetailChild_ClassScheduleModeSpace_classScheduleResults-component',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class MasterDetailChild_ClassScheduleModeSpace_ClassScheduleResultsComponent extends AppComponentBase {
    @Input('classScheduleModeSpaceId') classScheduleModeSpaceId: any;

    @ViewChild('entityTypeHistoryModal', { static: true }) entityTypeHistoryModal: EntityTypeHistoryModalComponent;
    @ViewChild('createOrEditClassScheduleResultModal', { static: true })
    createOrEditClassScheduleResultModal: MasterDetailChild_ClassScheduleModeSpace_CreateOrEditClassScheduleResultModalComponent;
    @ViewChild('viewClassScheduleResultModalComponent', { static: true })
    viewClassScheduleResultModal: MasterDetailChild_ClassScheduleModeSpace_ViewClassScheduleResultModalComponent;

    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';

    workTimeInDayFilter: string;
    lessonFilter: string;
    universityProfessorFilter: string;
    semesterFilter: string;
    gradeFilter: string;
    universityMajorFilter: string;
    universityDepartmentFilter: string;
    classroomBuildingFilter: string;
    classScheduleModeSpaceNameClassScheduleModeSpacesFilter = '';
    listOfAllCalculatedResultNameCalculatedResultFilter = '';
    isAlocated: boolean = true;

    _entityTypeFullName = 'Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleResults.ClassScheduleResult';
    entityHistoryEnabled = false;

    constructor(
        injector: Injector,
        private _classScheduleResultsServiceProxy: ClassScheduleResultsServiceProxy,
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

    getClassScheduleResults(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._classScheduleResultsServiceProxy
            .getAll(
                this.filterText,
                this.workTimeInDayFilter,
                this.lessonFilter ,
                this.universityProfessorFilter,
                this.semesterFilter,
                this.gradeFilter,
                this.universityMajorFilter,
                this.universityDepartmentFilter,
                this.classroomBuildingFilter,
                this.listOfAllCalculatedResultNameCalculatedResultFilter,
                this.classScheduleModeSpaceNameClassScheduleModeSpacesFilter,
                undefined,
                this.classScheduleModeSpaceId,
                this.isAlocated,
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

    createClassScheduleResult(): void {
        this.createOrEditClassScheduleResultModal.show(this.classScheduleModeSpaceId);
    }

    showHistory(classScheduleResult: ClassScheduleResultDto): void {
        this.entityTypeHistoryModal.show({
            entityId: classScheduleResult.id.toString(),
            entityTypeFullName: this._entityTypeFullName,
            entityTypeDescription: '',
        });
    }

    deleteClassScheduleResult(classScheduleResult: ClassScheduleResultDto): void {
        this.message.confirm('', this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._classScheduleResultsServiceProxy.delete(classScheduleResult.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    exportToExcel(): void {
        this._classScheduleResultsServiceProxy
            .getClassScheduleResultsToExcel(
                this.filterText,
                this.workTimeInDayFilter,
                this.lessonFilter ,
                this.universityProfessorFilter,
                this.semesterFilter,
                this.gradeFilter,
                this.universityMajorFilter,
                this.universityDepartmentFilter,
                this.classroomBuildingFilter,
                this.listOfAllCalculatedResultNameCalculatedResultFilter,
                this.classScheduleModeSpaceNameClassScheduleModeSpacesFilter,
                undefined,
                this.classScheduleModeSpaceId,
                this.isAlocated
            )
            .subscribe((result) => {
                this._fileDownloadService.downloadTempFile(result);
            });
    }
}
