﻿import { AppConsts } from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
    AssigningUniversityMajorToClassroomBuildingsServiceProxy,
    AssigningUniversityMajorToClassroomBuildingDto,
} from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditAssigningUniversityMajorToClassroomBuildingModalComponent } from './create-or-edit-assigningUniversityMajorToClassroomBuilding-modal.component';

import { ViewAssigningUniversityMajorToClassroomBuildingModalComponent } from './view-assigningUniversityMajorToClassroomBuilding-modal.component';
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
    templateUrl: './assigningUniversityMajorToClassroomBuildings.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class AssigningUniversityMajorToClassroomBuildingsComponent extends AppComponentBase {
    @ViewChild('entityTypeHistoryModal', { static: true }) entityTypeHistoryModal: EntityTypeHistoryModalComponent;
    @ViewChild('createOrEditAssigningUniversityMajorToClassroomBuildingModal', { static: true })
    createOrEditAssigningUniversityMajorToClassroomBuildingModal: CreateOrEditAssigningUniversityMajorToClassroomBuildingModalComponent;
    @ViewChild('viewAssigningUniversityMajorToClassroomBuildingModalComponent', { static: true })
    viewAssigningUniversityMajorToClassroomBuildingModal: ViewAssigningUniversityMajorToClassroomBuildingModalComponent;

    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    maxMaximumRestrictionsOnUsingClassroomsAtTheSameTimeFilter: number;
    maxMaximumRestrictionsOnUsingClassroomsAtTheSameTimeFilterEmpty: number;
    minMaximumRestrictionsOnUsingClassroomsAtTheSameTimeFilter: number;
    minMaximumRestrictionsOnUsingClassroomsAtTheSameTimeFilterEmpty: number;
    universityMajorUniversityMajorNameFilter = '';
    classroomBuildingClassroomBuildingNameFilter = '';

    _entityTypeFullName =
        'Mohajer.ClassScheduleProject.CentralUnit.AssigningUniversityMajorToClassroomBuildings.AssigningUniversityMajorToClassroomBuilding';
    entityHistoryEnabled = false;

    constructor(
        injector: Injector,
        private _assigningUniversityMajorToClassroomBuildingsServiceProxy: AssigningUniversityMajorToClassroomBuildingsServiceProxy,
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

    getAssigningUniversityMajorToClassroomBuildings(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._assigningUniversityMajorToClassroomBuildingsServiceProxy
            .getAll(
                this.filterText,
                this.maxMaximumRestrictionsOnUsingClassroomsAtTheSameTimeFilter == null
                    ? this.maxMaximumRestrictionsOnUsingClassroomsAtTheSameTimeFilterEmpty
                    : this.maxMaximumRestrictionsOnUsingClassroomsAtTheSameTimeFilter,
                this.minMaximumRestrictionsOnUsingClassroomsAtTheSameTimeFilter == null
                    ? this.minMaximumRestrictionsOnUsingClassroomsAtTheSameTimeFilterEmpty
                    : this.minMaximumRestrictionsOnUsingClassroomsAtTheSameTimeFilter,
                this.universityMajorUniversityMajorNameFilter,
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

    createAssigningUniversityMajorToClassroomBuilding(): void {
        this.createOrEditAssigningUniversityMajorToClassroomBuildingModal.show();
    }

    showHistory(assigningUniversityMajorToClassroomBuilding: AssigningUniversityMajorToClassroomBuildingDto): void {
        this.entityTypeHistoryModal.show({
            entityId: assigningUniversityMajorToClassroomBuilding.id.toString(),
            entityTypeFullName: this._entityTypeFullName,
            entityTypeDescription: '',
        });
    }

    deleteAssigningUniversityMajorToClassroomBuilding(
        assigningUniversityMajorToClassroomBuilding: AssigningUniversityMajorToClassroomBuildingDto
    ): void {
        this.message.confirm('', this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._assigningUniversityMajorToClassroomBuildingsServiceProxy
                    .delete(assigningUniversityMajorToClassroomBuilding.id)
                    .subscribe(() => {
                        this.reloadPage();
                        this.notify.success(this.l('SuccessfullyDeleted'));
                    });
            }
        });
    }

    exportToExcel(): void {
        this._assigningUniversityMajorToClassroomBuildingsServiceProxy
            .getAssigningUniversityMajorToClassroomBuildingsToExcel(
                this.filterText,
                this.maxMaximumRestrictionsOnUsingClassroomsAtTheSameTimeFilter == null
                    ? this.maxMaximumRestrictionsOnUsingClassroomsAtTheSameTimeFilterEmpty
                    : this.maxMaximumRestrictionsOnUsingClassroomsAtTheSameTimeFilter,
                this.minMaximumRestrictionsOnUsingClassroomsAtTheSameTimeFilter == null
                    ? this.minMaximumRestrictionsOnUsingClassroomsAtTheSameTimeFilterEmpty
                    : this.minMaximumRestrictionsOnUsingClassroomsAtTheSameTimeFilter,
                this.universityMajorUniversityMajorNameFilter,
                this.classroomBuildingClassroomBuildingNameFilter
            )
            .subscribe((result) => {
                this._fileDownloadService.downloadTempFile(result);
            });
    }
}
