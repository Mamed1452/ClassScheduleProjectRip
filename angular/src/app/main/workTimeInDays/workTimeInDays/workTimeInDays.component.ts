import { AppConsts } from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { WorkTimeInDaysServiceProxy, WorkTimeInDayDto, DayOfWeekEnum } from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditWorkTimeInDayModalComponent } from './create-or-edit-workTimeInDay-modal.component';

import { ViewWorkTimeInDayModalComponent } from './view-workTimeInDay-modal.component';
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
    templateUrl: './workTimeInDays.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class WorkTimeInDaysComponent extends AppComponentBase {
    @ViewChild('entityTypeHistoryModal', { static: true }) entityTypeHistoryModal: EntityTypeHistoryModalComponent;
    @ViewChild('createOrEditWorkTimeInDayModal', { static: true })
    createOrEditWorkTimeInDayModal: CreateOrEditWorkTimeInDayModalComponent;
    @ViewChild('viewWorkTimeInDayModalComponent', { static: true })
    viewWorkTimeInDayModal: ViewWorkTimeInDayModalComponent;

    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    nameWorkTimeInDayFilter = '';
    dayOfWeekFilter = -1;
    startTimeFilter = '';
    endTimeFilter = '';
    whatTimeOfDayFilter = '';

    dayOfWeekEnum = DayOfWeekEnum;

    _entityTypeFullName = 'Mohajer.ClassScheduleProject.CentralUnit.WorkTimeInDays.WorkTimeInDay';
    entityHistoryEnabled = false;
    createingAllWorkTimeInDay = false;
    constructor(
        injector: Injector,
        private _workTimeInDaysServiceProxy: WorkTimeInDaysServiceProxy,
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

    getWorkTimeInDays(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._workTimeInDaysServiceProxy
            .getAll(
                this.filterText,
                this.nameWorkTimeInDayFilter,
                this.dayOfWeekFilter,
                this.startTimeFilter,
                this.endTimeFilter,
                this.whatTimeOfDayFilter,
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

    createWorkTimeInDay(): void {
        this.createOrEditWorkTimeInDayModal.show();
    }

    showHistory(workTimeInDay: WorkTimeInDayDto): void {
        this.entityTypeHistoryModal.show({
            entityId: workTimeInDay.id.toString(),
            entityTypeFullName: this._entityTypeFullName,
            entityTypeDescription: '',
        });
    }

    deleteWorkTimeInDay(workTimeInDay: WorkTimeInDayDto): void {
        this.message.confirm('', this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._workTimeInDaysServiceProxy.delete(workTimeInDay.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    exportToExcel(): void {
        this._workTimeInDaysServiceProxy
            .getWorkTimeInDaysToExcel(
                this.filterText,
                this.nameWorkTimeInDayFilter,
                this.dayOfWeekFilter,
                this.startTimeFilter,
                this.endTimeFilter,
                this.whatTimeOfDayFilter
            )
            .subscribe((result) => {
                this._fileDownloadService.downloadTempFile(result);
            });
    }
    createallWorkTimeInDay()
    {
        this.createingAllWorkTimeInDay=true;
        this._workTimeInDaysServiceProxy
        .createAllWorkTimeInDay()
        .subscribe(()=>
        {
            this.reloadPage();
            this.notify.success(this.l('SavedSuccessfully'));
            this.createingAllWorkTimeInDay=false;
        });
    }
}
