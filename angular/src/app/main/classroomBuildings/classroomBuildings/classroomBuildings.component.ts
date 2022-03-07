import { AppConsts } from '@shared/AppConsts';
import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
    ClassroomBuildingsServiceProxy,
    ClassroomBuildingDto,
    ClassificationClassroomBuildingEnum,
} from '@shared/service-proxies/service-proxies';
import { NotifyService } from 'abp-ng2-module';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditClassroomBuildingModalComponent } from './create-or-edit-classroomBuilding-modal.component';

import { ViewClassroomBuildingModalComponent } from './view-classroomBuilding-modal.component';
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
    templateUrl: './classroomBuildings.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
})
export class ClassroomBuildingsComponent extends AppComponentBase {
    @ViewChild('entityTypeHistoryModal', { static: true }) entityTypeHistoryModal: EntityTypeHistoryModalComponent;
    @ViewChild('createOrEditClassroomBuildingModal', { static: true })
    createOrEditClassroomBuildingModal: CreateOrEditClassroomBuildingModalComponent;
    @ViewChild('viewClassroomBuildingModalComponent', { static: true })
    viewClassroomBuildingModal: ViewClassroomBuildingModalComponent;

    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    classroomBuildingNameFilter = '';
    maxNumberOfClassroomsFilter: number;
    maxNumberOfClassroomsFilterEmpty: number;
    minNumberOfClassroomsFilter: number;
    minNumberOfClassroomsFilterEmpty: number;
    classificationClassroomBuildingFilter = -1;

    classificationClassroomBuildingEnum = ClassificationClassroomBuildingEnum;

    _entityTypeFullName = 'Mohajer.ClassScheduleProject.CentralUnit.ClassroomBuildings.ClassroomBuilding';
    entityHistoryEnabled = false;

    constructor(
        injector: Injector,
        private _classroomBuildingsServiceProxy: ClassroomBuildingsServiceProxy,
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

    getClassroomBuildings(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._classroomBuildingsServiceProxy
            .getAll(
                this.filterText,
                this.classroomBuildingNameFilter,
                this.maxNumberOfClassroomsFilter == null
                    ? this.maxNumberOfClassroomsFilterEmpty
                    : this.maxNumberOfClassroomsFilter,
                this.minNumberOfClassroomsFilter == null
                    ? this.minNumberOfClassroomsFilterEmpty
                    : this.minNumberOfClassroomsFilter,
                this.classificationClassroomBuildingFilter,
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

    createClassroomBuilding(): void {
        this.createOrEditClassroomBuildingModal.show();
    }

    showHistory(classroomBuilding: ClassroomBuildingDto): void {
        this.entityTypeHistoryModal.show({
            entityId: classroomBuilding.id.toString(),
            entityTypeFullName: this._entityTypeFullName,
            entityTypeDescription: '',
        });
    }

    deleteClassroomBuilding(classroomBuilding: ClassroomBuildingDto): void {
        this.message.confirm('', this.l('AreYouSure'), (isConfirmed) => {
            if (isConfirmed) {
                this._classroomBuildingsServiceProxy.delete(classroomBuilding.id).subscribe(() => {
                    this.reloadPage();
                    this.notify.success(this.l('SuccessfullyDeleted'));
                });
            }
        });
    }

    exportToExcel(): void {
        this._classroomBuildingsServiceProxy
            .getClassroomBuildingsToExcel(
                this.filterText,
                this.classroomBuildingNameFilter,
                this.maxNumberOfClassroomsFilter == null
                    ? this.maxNumberOfClassroomsFilterEmpty
                    : this.maxNumberOfClassroomsFilter,
                this.minNumberOfClassroomsFilter == null
                    ? this.minNumberOfClassroomsFilterEmpty
                    : this.minNumberOfClassroomsFilter,
                this.classificationClassroomBuildingFilter
            )
            .subscribe((result) => {
                this._fileDownloadService.downloadTempFile(result);
            });
    }
}
