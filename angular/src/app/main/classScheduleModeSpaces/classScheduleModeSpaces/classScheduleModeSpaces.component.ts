import { AppConsts } from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
    ClassScheduleModeSpacesServiceProxy,
    ClassScheduleModeSpaceDto,
} from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditClassScheduleModeSpaceModalComponent } from './create-or-edit-classScheduleModeSpace-modal.component';

import { ViewClassScheduleModeSpaceModalComponent } from './view-classScheduleModeSpace-modal.component';
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
    templateUrl: './classScheduleModeSpaces.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class ClassScheduleModeSpacesComponent extends AppComponentBase {
    @ViewChild('entityTypeHistoryModal', { static: true }) entityTypeHistoryModal: EntityTypeHistoryModalComponent;
    @ViewChild('createOrEditClassScheduleModeSpaceModal', { static: true })
    createOrEditClassScheduleModeSpaceModal: CreateOrEditClassScheduleModeSpaceModalComponent;
    @ViewChild('viewClassScheduleModeSpaceModalComponent', { static: true })
    viewClassScheduleModeSpaceModal: ViewClassScheduleModeSpaceModalComponent;

    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    nameClassScheduleModeSpacesFilter = '';
    isLockFilter = -1;
    listOfClassScheduleModeSpaceListOfClassScheduleModeSpaceNameFilter = '';
    universityProfessorUniversityProfessorNameFilter = '';
    workTimeInDayNameWorkTimeInDayFilter = '';
    lessonNameLessonFilter = '';

    _entityTypeFullName = 'Mohajer.ClassScheduleProject.CentralUnit.ClassScheduleModeSpaces.ClassScheduleModeSpace';
    entityHistoryEnabled = false;

    classScheduleResultRowSelection: boolean[] = [];

    childEntitySelection: {} = {};

    constructor(
        injector: Injector,
        private _classScheduleModeSpacesServiceProxy: ClassScheduleModeSpacesServiceProxy,
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

    getClassScheduleModeSpaces(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._classScheduleModeSpacesServiceProxy
            .getAll(
                this.filterText,
                this.nameClassScheduleModeSpacesFilter,
                this.isLockFilter,
                this.listOfClassScheduleModeSpaceListOfClassScheduleModeSpaceNameFilter,
                this.universityProfessorUniversityProfessorNameFilter,
                this.workTimeInDayNameWorkTimeInDayFilter,
                this.lessonNameLessonFilter,
                undefined,
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

    createClassScheduleModeSpace(): void {
        this.createOrEditClassScheduleModeSpaceModal.show();
    }

    showHistory(classScheduleModeSpace: ClassScheduleModeSpaceDto): void {
        this.entityTypeHistoryModal.show({
            entityId: classScheduleModeSpace.id.toString(),
            entityTypeFullName: this._entityTypeFullName,
            entityTypeDescription: '',
        });
    }

    deleteClassScheduleModeSpace(classScheduleModeSpace: ClassScheduleModeSpaceDto): void {
        this.message.confirm('', this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._classScheduleModeSpacesServiceProxy.delete(classScheduleModeSpace.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    exportToExcel(): void {
        this._classScheduleModeSpacesServiceProxy
            .getClassScheduleModeSpacesToExcel(
                this.filterText,
                this.nameClassScheduleModeSpacesFilter,
                this.isLockFilter,
                this.listOfClassScheduleModeSpaceListOfClassScheduleModeSpaceNameFilter,
                this.universityProfessorUniversityProfessorNameFilter,
                this.workTimeInDayNameWorkTimeInDayFilter,
                this.lessonNameLessonFilter,
                undefined
            )
            .subscribe((result) => {
                this._fileDownloadService.downloadTempFile(result);
            });
    }

    selectEditTable(table) {
        this.childEntitySelection = {};
        this.childEntitySelection[table] = true;
    }

    openChildRowForClassScheduleResult(index, table) {
        let isAlreadyOpened = this.classScheduleResultRowSelection[index];
        this.closeAllChildRows();
        this.classScheduleResultRowSelection = [];
        if (!isAlreadyOpened) {
            this.classScheduleResultRowSelection[index] = true;
        }
        this.selectEditTable(table);
    }

    closeAllChildRows(): void {
        this.classScheduleResultRowSelection = [];
    }
}
