import { AppConsts } from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ListOfMainDomainsServiceProxy, ListOfMainDomainDto } from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditListOfMainDomainModalComponent } from './create-or-edit-listOfMainDomain-modal.component';

import { ViewListOfMainDomainModalComponent } from './view-listOfMainDomain-modal.component';
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
    templateUrl: './listOfMainDomains.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class ListOfMainDomainsComponent extends AppComponentBase {
    @ViewChild('entityTypeHistoryModal', { static: true }) entityTypeHistoryModal: EntityTypeHistoryModalComponent;
    @ViewChild('createOrEditListOfMainDomainModal', { static: true })
    createOrEditListOfMainDomainModal: CreateOrEditListOfMainDomainModalComponent;
    @ViewChild('viewListOfMainDomainModalComponent', { static: true })
    viewListOfMainDomainModal: ViewListOfMainDomainModalComponent;

    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    listOfMainDomainNameFilter = '';

    _entityTypeFullName = 'Mohajer.ClassScheduleProject.CentralUnit.ListOfMainDomains.ListOfMainDomain';
    entityHistoryEnabled = false;

    mainDomainRowSelection: boolean[] = [];

    childEntitySelection: {} = {};

    constructor(
        injector: Injector,
        private _listOfMainDomainsServiceProxy: ListOfMainDomainsServiceProxy,
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

    getListOfMainDomains(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._listOfMainDomainsServiceProxy
            .getAll(
                this.filterText,
                this.listOfMainDomainNameFilter,
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

    createListOfMainDomain(): void {
        this.createOrEditListOfMainDomainModal.show();
    }

    showHistory(listOfMainDomain: ListOfMainDomainDto): void {
        this.entityTypeHistoryModal.show({
            entityId: listOfMainDomain.id.toString(),
            entityTypeFullName: this._entityTypeFullName,
            entityTypeDescription: '',
        });
    }

    deleteListOfMainDomain(listOfMainDomain: ListOfMainDomainDto): void {
        this.message.confirm('', this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._listOfMainDomainsServiceProxy.delete(listOfMainDomain.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    exportToExcel(): void {
        this._listOfMainDomainsServiceProxy
            .getListOfMainDomainsToExcel(this.filterText, this.listOfMainDomainNameFilter)
            .subscribe((result) => {
                this._fileDownloadService.downloadTempFile(result);
            });
    }

    selectEditTable(table) {
        this.childEntitySelection = {};
        this.childEntitySelection[table] = true;
    }

    openChildRowForMainDomain(index, table) {
        let isAlreadyOpened = this.mainDomainRowSelection[index];
        this.closeAllChildRows();
        this.mainDomainRowSelection = [];
        if (!isAlreadyOpened) {
            this.mainDomainRowSelection[index] = true;
        }
        this.selectEditTable(table);
    }

    closeAllChildRows(): void {
        this.mainDomainRowSelection = [];
    }
}
