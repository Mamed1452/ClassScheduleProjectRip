﻿import { AppConsts } from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MainDomainsServiceProxy, MainDomainDto } from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditMainDomainModalComponent } from './create-or-edit-mainDomain-modal.component';

import { ViewMainDomainModalComponent } from './view-mainDomain-modal.component';
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
    templateUrl: './mainDomains.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class MainDomainsComponent extends AppComponentBase {
    @ViewChild('entityTypeHistoryModal', { static: true }) entityTypeHistoryModal: EntityTypeHistoryModalComponent;
    @ViewChild('createOrEditMainDomainModal', { static: true })
    createOrEditMainDomainModal: CreateOrEditMainDomainModalComponent;
    @ViewChild('viewMainDomainModalComponent', { static: true }) viewMainDomainModal: ViewMainDomainModalComponent;

    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    mainDomainNameFilter = '';
    listOfMainDomainListOfMainDomainNameFilter = '';
    lessonsOfSemesterLessonsOfSemesterNameFilter = '';

    _entityTypeFullName = 'Mohajer.ClassScheduleProject.CentralUnit.MainDomains.MainDomain';
    entityHistoryEnabled = false;

    constructor(
        injector: Injector,
        private _mainDomainsServiceProxy: MainDomainsServiceProxy,
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

    getMainDomains(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._mainDomainsServiceProxy
            .getAll(
                this.filterText,
                this.mainDomainNameFilter,
                this.listOfMainDomainListOfMainDomainNameFilter,
                this.lessonsOfSemesterLessonsOfSemesterNameFilter,
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

    createMainDomain(): void {
        this.createOrEditMainDomainModal.show();
    }

    showHistory(mainDomain: MainDomainDto): void {
        this.entityTypeHistoryModal.show({
            entityId: mainDomain.id.toString(),
            entityTypeFullName: this._entityTypeFullName,
            entityTypeDescription: '',
        });
    }

    deleteMainDomain(mainDomain: MainDomainDto): void {
        this.message.confirm('', this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._mainDomainsServiceProxy.delete(mainDomain.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    exportToExcel(): void {
        this._mainDomainsServiceProxy
            .getMainDomainsToExcel(
                this.filterText,
                this.mainDomainNameFilter,
                this.listOfMainDomainListOfMainDomainNameFilter,
                this.lessonsOfSemesterLessonsOfSemesterNameFilter,
                undefined
            )
            .subscribe((result) => {
                this._fileDownloadService.downloadTempFile(result);
            });
    }
}
