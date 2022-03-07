import { AppConsts } from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
    UniversityMajorsServiceProxy,
    UniversityMajorDto,
    UniversityMajorTypeEnum,
} from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditUniversityMajorModalComponent } from './create-or-edit-universityMajor-modal.component';

import { ViewUniversityMajorModalComponent } from './view-universityMajor-modal.component';
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
    templateUrl: './universityMajors.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class UniversityMajorsComponent extends AppComponentBase {
    @ViewChild('entityTypeHistoryModal', { static: true }) entityTypeHistoryModal: EntityTypeHistoryModalComponent;
    @ViewChild('createOrEditUniversityMajorModal', { static: true })
    createOrEditUniversityMajorModal: CreateOrEditUniversityMajorModalComponent;
    @ViewChild('viewUniversityMajorModalComponent', { static: true })
    viewUniversityMajorModal: ViewUniversityMajorModalComponent;

    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    universityMajorNameFilter = '';
    universityMajorTypeFilter = -1;
    universityDepartmentUniversityDepartmentNameFilter = '';
    universityDepartmentUniversityDepartmentName2Filter = '';

    universityMajorTypeEnum = UniversityMajorTypeEnum;

    _entityTypeFullName = 'Mohajer.ClassScheduleProject.CentralUnit.UniversityMajors.UniversityMajor';
    entityHistoryEnabled = false;

    constructor(
        injector: Injector,
        private _universityMajorsServiceProxy: UniversityMajorsServiceProxy,
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

    getUniversityMajors(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._universityMajorsServiceProxy
            .getAll(
                this.filterText,
                this.universityMajorNameFilter,
                this.universityMajorTypeFilter,
                this.universityDepartmentUniversityDepartmentNameFilter,
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

    createUniversityMajor(): void {
        this.createOrEditUniversityMajorModal.show();
    }

    showHistory(universityMajor: UniversityMajorDto): void {
        this.entityTypeHistoryModal.show({
            entityId: universityMajor.id.toString(),
            entityTypeFullName: this._entityTypeFullName,
            entityTypeDescription: '',
        });
    }

    deleteUniversityMajor(universityMajor: UniversityMajorDto): void {
        this.message.confirm('', this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._universityMajorsServiceProxy.delete(universityMajor.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    exportToExcel(): void {
        this._universityMajorsServiceProxy
            .getUniversityMajorsToExcel(
                this.filterText,
                this.universityMajorNameFilter,
                this.universityMajorTypeFilter,
                this.universityDepartmentUniversityDepartmentNameFilter
            )
            .subscribe((result) => {
                this._fileDownloadService.downloadTempFile(result);
            });
    }
}
