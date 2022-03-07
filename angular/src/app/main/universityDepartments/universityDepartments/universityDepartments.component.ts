import { AppConsts } from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UniversityDepartmentsServiceProxy, UniversityDepartmentDto } from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditUniversityDepartmentModalComponent } from './create-or-edit-universityDepartment-modal.component';

import { ViewUniversityDepartmentModalComponent } from './view-universityDepartment-modal.component';
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
    templateUrl: './universityDepartments.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class UniversityDepartmentsComponent extends AppComponentBase {
    @ViewChild('entityTypeHistoryModal', { static: true }) entityTypeHistoryModal: EntityTypeHistoryModalComponent;
    @ViewChild('createOrEditUniversityDepartmentModal', { static: true })
    createOrEditUniversityDepartmentModal: CreateOrEditUniversityDepartmentModalComponent;
    @ViewChild('viewUniversityDepartmentModalComponent', { static: true })
    viewUniversityDepartmentModal: ViewUniversityDepartmentModalComponent;

    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    universityDepartmentNameFilter = '';

    _entityTypeFullName = 'Mohajer.ClassScheduleProject.CentralUnit.UniversityDepartments.UniversityDepartment';
    entityHistoryEnabled = false;

    universityMajorRowSelection: boolean[] = [];

    childEntitySelection: {} = {};

    constructor(
        injector: Injector,
        private _universityDepartmentsServiceProxy: UniversityDepartmentsServiceProxy,
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

    getUniversityDepartments(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._universityDepartmentsServiceProxy
            .getAll(
                this.filterText,
                this.universityDepartmentNameFilter,
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

    createUniversityDepartment(): void {
        this.createOrEditUniversityDepartmentModal.show();
    }

    showHistory(universityDepartment: UniversityDepartmentDto): void {
        this.entityTypeHistoryModal.show({
            entityId: universityDepartment.id.toString(),
            entityTypeFullName: this._entityTypeFullName,
            entityTypeDescription: '',
        });
    }

    deleteUniversityDepartment(universityDepartment: UniversityDepartmentDto): void {
        this.message.confirm('', this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._universityDepartmentsServiceProxy.delete(universityDepartment.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    exportToExcel(): void {
        this._universityDepartmentsServiceProxy
            .getUniversityDepartmentsToExcel(this.filterText, this.universityDepartmentNameFilter)
            .subscribe((result) => {
                this._fileDownloadService.downloadTempFile(result);
            });
    }

    selectEditTable(table) {
        this.childEntitySelection = {};
        this.childEntitySelection[table] = true;
    }

    openChildRowForUniversityMajor(index, table) {
        let isAlreadyOpened = this.universityMajorRowSelection[index];
        this.closeAllChildRows();
        this.universityMajorRowSelection = [];
        if (!isAlreadyOpened) {
            this.universityMajorRowSelection[index] = true;
        }
        this.selectEditTable(table);
    }

    closeAllChildRows(): void {
        this.universityMajorRowSelection = [];
    }
}
